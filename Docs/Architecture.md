# Architecture

The general idea is that each change is stored as a delta. To reach one point in history, you need to replay the events. This can be optimized with snapshots.

## Storage

Changes are managed using three types of storage:
1. a log of the changes
2. an index to look up changes by ID
3. the current state

Each branch has its own log and index. The beginning of the log has a reference to its base, so that logs can be chained together. The change IDs are unique across branches to make merging simpler.

## Snapshots

The history replays are expensive to perform, so we need a cache of snapshots. Snapshots should not duplicate unchanged file content, so the cache is layered. There are two layers:
1. a file layer that maps file hashes to (compressed) content
2. a change layer index that maps change ID to changed file hashes

The change layer itself can be delta compressed, so that we don't list all file hashes in every entry.
