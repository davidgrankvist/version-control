# Architecture

The general idea is that each change is stored as a delta. To reach one point in history, you need to replay the events. This can be optimized with snapshots.

## Storage

The storage is a mixture of change logs, index files and caches.

### Primary storage

Changes are managed using three types of storage:
1. a log of the changes
2. an index to look up changes by ID
3. the current state

Each branch has its own log and index. The beginning of the log has a reference to its base, so that logs can be chained together. The change IDs are unique across branches to make merging simpler.

### Snapshots

The history replays are expensive to perform, so we need a cache of snapshots. Snapshots should not duplicate unchanged file content, so the cache is layered. There are two layers:
1. a file layer that maps file hashes to (compressed) content
2. a change layer index that maps change ID to changed file hashes

The change layer itself can be delta compressed, so that we don't list all file hashes in every entry.

### Detecting changes

Before we store the deltas, we need to detect which files to calculate the deltas for. Only a few files may have been edited, so calculating hashes for all of the files would be very inefficient.

To deal with this problem, there is another cache: the file metadata cache. When a file is unpacked, its metadata such as last modified timestamp is stored. Later when the file tree is walked, only entries with updated metadata are considered updated.

### Delta calculation

The delta is represented as a series of operations to transform a file, for example adding or removing one line. The diff algorithm has two major steps:
1. figure out which parts are changed
2. represent the change as a small number of operations

The first part is solved by finding the [Longest Common Subsequence (LCS)](https://en.wikipedia.org/wiki/Longest_common_subsequence). When the LCS is removed from the old and new versions of the file, then what remains is the delta.

The second part can be solved naively by treating the remainder of the old file as removals and the remainder of the new file as additions.
