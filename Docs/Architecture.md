# Architecture

The general idea is that each change is stored as a delta. To reach one point in history, you need to replay the events. This can be optimized with snapshots.

## Storage

Changes are managed using three types of files:
1. a log of the changes
2. an index to look up changes by ID
3. the current state

Each branch has its own log and index. The beginning of the log has a reference to its base, so that logs can be chained together. The change IDs are unique across branches to make merging simpler.
