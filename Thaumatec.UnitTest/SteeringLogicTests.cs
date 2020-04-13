using Thaumatec.Core.Program.Result;
using Thaumatec.Core.SteeringLogic.CalculateStep;
using Thaumatec.Core.SteeringLogic.Information;
using Xunit;

namespace Thaumatec.UnitTest
{
    [Trait("Category", "SteeringLogic")]
    public class SteeringLogicTests
    {
        [Fact(DisplayName = "When pallet need to perform next step")]
        public void PalletNeedToPerformNextStep()
        {
            var processing = new SteeringLogicProcessing(new SteeringLogicRequest(1, new ProgramStepResult(1, 1, 1, 1, 1, 1, "1", new NodaTime.LocalDateTime(2020, 5, 20, 20, 20))), new SteeringLogicStepInformation(1, 5));

            Assert.Equal(SteeringLogicState.NextStep, processing.State);
        }
    }
}
