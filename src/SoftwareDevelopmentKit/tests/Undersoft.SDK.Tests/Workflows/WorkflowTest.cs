namespace Undersoft.SDK.Tests.Workflows
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Text;
    using Undersoft.SDK.Invoking;
    using Undersoft.SDK.Tests.Workflows.Features;
    using Undersoft.SDK.Workflows;

    [TestClass]
    public class WorkflowTest
    {
        public WorkflowTest()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [TestMethod]
        public void Workflow_MultiThreading_ParallelConcurrentSynchronization_Framework_Integration_Test()
        {
            var work = new Workflow();

            var download = work
                .Aspect<BankCurrencyService>()
                    .AddWork<FirstCurrency>((w) => w.GetCurrency)
                    .AddWork<SecondCurrency>((w) => w.GetCurrency)
                .Allocate(4);

            var compute = work
                .Aspect<WorkflowTest>()
                    .AddWork<ComputeCurrency>((w) => w.Compute)
                    .AddWork<PresentResult>((w) => w.Present)
                .Allocate(2);

            download
                .Work<FirstCurrency>((w) => w.GetCurrency)
                    .FlowTo<ComputeCurrency>((w) => w.Compute)
                .Work<SecondCurrency>((w) => w.GetCurrency)
                    .FlowTo<ComputeCurrency>((w) => w.Compute);

            compute
                .Work<PresentResult>((w) => w.Present)
                    .FlowFrom<ComputeCurrency>((w) => w.Compute);

            for (int i = 1; i < 7; i++)
            {
                download
                    .Work<FirstCurrency>((w) => w.GetCurrency).Start("EUR", i)
                    .Work<SecondCurrency>((w) => w.GetCurrency).Start("USD", i);
            }

            Task.Delay(5000);

            download.Close(true);
            compute.Close(true);
        }

        [TestMethod]
        public void Workout_Integration_Test()
        {
            var ql0 = new Workout(new Invoker<FirstCurrency>(), "EUR", 1);
            var ql1 = new Workout(new Invoker<SecondCurrency>(), "USD", 1);

            ql0 = Workout.Run<FirstCurrency>(true, "EUR", 1);
            ql1 = Workout.Run<SecondCurrency>(false, "USD", 1);

            Task.Delay(5000);
        }
    }
}
