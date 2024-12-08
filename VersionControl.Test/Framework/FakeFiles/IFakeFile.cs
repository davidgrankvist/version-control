namespace VersionControl.Test.Framework.FakeFiles;
public interface IFakeFile
{
    Stream Write();

    Stream Read();

    byte[] ReadAllBytes();
}
