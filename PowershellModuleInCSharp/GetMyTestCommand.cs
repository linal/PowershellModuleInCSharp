using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Threading;

namespace PowershellModuleInCSharp
{
    [Cmdlet(VerbsCommon.Get, "MyTestCommand")]
    [OutputType(typeof(TestObject))]
    public class GetMyTestCommand : Cmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }
        protected override void BeginProcessing()
        {
            Console.WriteLine("BeginProcessing");
        }

        protected override void ProcessRecord()
        {
            //WriteInformation(new InformationRecord("data", "source"));

            WriteDebug("Debug");

            ProgressRecord myprogress = new ProgressRecord(1, "Testing", "Progress:");

            for (int i = 0; i < 10; i++)
            {
                myprogress.PercentComplete = i;
                Thread.Sleep(10);
                WriteProgress(myprogress);
                WriteObject(new TestObject { Message = $"Message {i}" });
            }


            WriteCommandDetail("CommandDetail");
            WriteVerbose("Versbose");
            WriteWarning("Warning!");

            //WriteError(new ErrorRecord(new Exception("Error"), "1", ErrorCategory.WriteError, null));
        }

    }

    [Cmdlet("Write", "MyTestCommand")]
    public class WriteMyTestCommand : Cmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public List<TestObject> Input { get; set; }

        protected override void ProcessRecord()
        {
            base.ProcessRecord();
            if (Input != null)
            {
                foreach (var testObject in Input)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"[{testObject.Created:G}] {testObject.Message}");
                }

                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("No Obects");
            }
        }
    }
    public class TestObject
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public string Message { get; set; }
    }
}