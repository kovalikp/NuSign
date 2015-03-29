namespace NuSign.Tasks
{
    using System.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// 
    /// </summary>
    public class SignTool : ToolTask
    {
        public ITaskItem SignCertificateFile { get; set; }

        [Required]
        public ITaskItem FileName { get; set; }

        public string TimestampServerUrl { get; set; }

        protected override string ToolName
        {
            get { return "signtool.exe"; }
        }

        protected override string GenerateCommandLineCommands()
        {
            var builder = new CommandLineBuilder();
            builder.AppendSwitch("sign");

            builder.AppendSwitchIfNotNull("/f ", SignCertificateFile.GetMetadata("FullPath"));

            builder.AppendSwitchIfNotNull("/t ", TimestampServerUrl);

            builder.AppendFileNameIfNotNull(FileName.GetMetadata("FullPath"));

            return builder.ToString();
        }

        protected override string GenerateFullPathToTool()
        {
            return Path.Combine(ToolPath, ToolName);
        }
    }
}