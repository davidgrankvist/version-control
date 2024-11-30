# version-control 
Version control. 

## About

A version control system with a CLI. The purpose of this project is to understand version control system internals better by implementing one.

### Architecture

This is a work in progress, but the general idea is that each change is stored as a delta. To reach one point in history, you need to replay the events. This can be optimized with snapshots.

### Commands

This part will likely change, but here goes:

```
$ vcs --help
vcs - A version control tool.

Control your versions.

Commands:
compare - Compare changes.
goto    - Time travel.
history - View history.
save    - Save changes.
status  - List changes.
```
