using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Xunit;

public class ChoreComponentTests : TestContext
{
    [Fact]
    public async Task SubmitForm_CallsAddChore_WhenChoreReadIdIsZero()
    {
        var renederComponent = RenderComponent<AddEditChore>();
    }
    
}