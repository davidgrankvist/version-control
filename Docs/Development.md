# Development

Here are some development guides so I don't forget. Enjoy.

## Testing

### Manual testing

To make manual testing simpler, there is a workspace and some scripts.

To test manually:
- navigate to `testroot`
- use `run.bat` as the CLI entrypoint
- reset the workspace with `clean.ps1`

Example:
```
cd testroot
echo "hello" > hello.txt
run.bat save hello.txt -m "add hello"
run.bat history
# prints the change

# later when you're done
clean.ps1
```
