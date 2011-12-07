namespace CR_ImportNamespace
{
  public interface IAssemblyScanProgress
  {
    void Start(int fileCount);
    void UpdateProgress(int fileIndex, string text);
    void Stop();
  }
}