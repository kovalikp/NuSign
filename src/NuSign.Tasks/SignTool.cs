namespace NuSign.Tasks
{
    using System;
    using System.IO;
    using Microsoft.Build.Framework;
    using Microsoft.Build.Utilities;

    /// <summary>
    /// Uses the "signtool.exe" tool to sign files using embedded signatures.
    /// </summary>
    public class SignTool : ToolTask
    {
        [Required]
        public ITaskItem FileName { get; set; }

        public ITaskItem SignCertificateFile { get; set; }

        public string TimestampServerUrl { get; set; }

        protected override string ToolName
        {
            get { return "signtool.exe"; }
        }

        protected override string GenerateCommandLineCommands()
        {
            var builder = new CommandLineBuilder();
            builder.AppendSwitch("sign");

            builder.AppendSwitchIfNotNull("/f ", SignCertificateFile);

            builder.AppendSwitchIfNotNull("/t ", TimestampServerUrl);

            builder.AppendFileNameIfNotNull(FileName);

            return builder.ToString();
        }

        protected override string GenerateFullPathToTool()
        {
            var windowsSdkLocation = ToolLocationHelper.GetPlatformSDKLocation("Windows", "8.1");
            if (string.IsNullOrEmpty(windowsSdkLocation))
            {
                windowsSdkLocation = ToolLocationHelper.GetPlatformSDKLocation("Windows", "8.0");
            }

            var architectureDir = "";
            switch (ProcessorArchitecture.CurrentProcessArchitecture)
            {
                case ProcessorArchitecture.AMD64:
                    architectureDir = "x64";
                    break;

                case ProcessorArchitecture.X86:
                    architectureDir = "x86";
                    break;

                case ProcessorArchitecture.ARM:
                    architectureDir = "arm";
                    break;

                case ProcessorArchitecture.IA64:
                    throw new InvalidOperationException("IA64 architecture not supported.");
                default:
                    throw new InvalidOperationException("Unknown architecture not supported.");
            }

            return Path.Combine(windowsSdkLocation, "bin", architectureDir, ToolName);
        }
    }
}