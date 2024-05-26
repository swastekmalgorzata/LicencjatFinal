using System.Configuration;
using Bunit;
using Xunit;
using ApartmentManagmentBlazorAppCopy.Components.Chores;
using ApartmentManagmentBlazorAppCopy.Components.CreateApartmentJoinApartment;
using ApartmentManagmentBlazorAppCopy.Components.Dashboard;
using ApartmentManagmentBlazorAppCopy.Components.Meter;
using ApartmentManagmentBlazorAppCopy.Components.Purchases;
using ApartmentManagmentBlazorAppCopy.Models;
using ApartmentManagmentBlazorAppCopy.Requests.ApartmentRequests;
using ApartmentManagmentBlazorAppCopy.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Tests.bUnit;
using Bunit.Rendering;
using Bunit.TestDoubles;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NuGet.Protocol.Plugins;
using TestContext = Bunit.TestContext;

namespace TestProject3;


public class UnitTest1 : TestContext
{
    private ServiceProvider _serviceProvider;

    public UnitTest1()
    {
        var serviceCollection = new ServiceCollection();
        Services
            .AddBlazoriseTests()
            .AddBootstrapProviders()
            .AddEmptyIconProvider();
        
        _serviceProvider = serviceCollection.BuildServiceProvider();
        JSInterop.AddBlazoriseButton()
            .AddBlazoriseTextEdit()
            .AddBlazoriseTable();
    }

    [Fact]
    public void Default_State_For_Component()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);

        var choreServices = new Mock<IChoresService>();
        Services.AddSingleton(choreServices.Object);

        var component = RenderComponent<ChoresList>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );
        
        component.Find("h3").MarkupMatches("<h3>Chores</h3>");
    }

    [Fact]
    public void Chore_Lists_Table_Contains_ChoreNames()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);
        var chores = new List<Chore>
        {
            new Chore { Id = 1, ChoreName = "Chore1", ApartmentId = 1, IsDone = false},
            new Chore { Id = 2, ChoreName = "Chore2", ApartmentId = 1, IsDone = false}
        };
        // Arrange
        var mockChoreServices = new Mock<IChoresService>();
        mockChoreServices.Setup(x => x.GetChoresAsync(1)).ReturnsAsync(chores);
        Services.AddSingleton(mockChoreServices.Object);
    
        var component = RenderComponent<ChoresList>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );


        component.Find("h3").MarkupMatches("<h3>Chores</h3>");
        var rows = component.FindAll("table tbody tr");
        
        Assert.Equal(2, rows.Count);

        Assert.Contains(rows, row => row.InnerHtml.Contains("Chore1"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("Chore2"));
        
    }
    [Fact]
    public void Meters_Table_Contains_Meteres_Readings()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);
        var meters = new List<Meter>
        {
            new Meter() { Id = 1, EnrgyReading = 1, ApartmentId = 1, Month = "test1"},
            new Meter() { Id = 2, GasReading = 1, ApartmentId = 1, Month = "test2"},
            new Meter() { Id = 3, WaterReading = 1, ApartmentId = 1, Month = "test3"},

        };
        // Arrange
        var mockChoreServices = new Mock<IMeterServices>();
        mockChoreServices.Setup(x => x.GetMeters(1)).Returns(meters);
        Services.AddSingleton(mockChoreServices.Object);
    
        var component = RenderComponent<MetersList>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );


        component.Find("h3").MarkupMatches("<h3>Meters readings</h3>");
        var rows = component.FindAll("table tbody tr");
        
        Assert.Equal(3, rows.Count);

        Assert.Contains(rows, row => row.InnerHtml.Contains("test1"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("test2"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("test3"));

        
    }
    [Fact]
    public void Adding_Meter()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);
        // Arrange
        var meters = new List<Meter>();

        var mockChoreServices = new Mock<IMeterServices>();
        mockChoreServices.Setup(x => x.GetMeters(1)).Returns(meters);
        
        mockChoreServices.Setup(x => x.AddMeter(It.IsAny<long>(),It.IsAny<AddMeterRequest>()))
            .Callback<long, AddMeterRequest>((apartmentId, meter) => meters.Add(new Meter()
            {
                ApartmentId = 1,
                Month = meter.Month,
                EnrgyReading = meter.EnergyReading,
                GasReading = meter.GasReading,
                WaterReading = meter.WaterReading
            }))
            .ReturnsAsync(1);
    
        Services.AddSingleton(mockChoreServices.Object);

        var component = RenderComponent<EditAddNewMeterReading>(parameters => parameters.Add(p => p.ApartmentId, 1));
        

        component.Find("#Month").Change("Month");
        component.Find("#water").Input(1);
        component.Find("#gas").Input(2);
        component.Find("#energy").Input(3);

        component.Find("#buttonSubmit").Click();
        
        var meterList = RenderComponent<MetersList>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );
        
        var rows = meterList.FindAll("table tbody tr");
        
        Assert.Single(rows);
        Assert.Contains(rows, row => row.InnerHtml.Contains("1"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("2"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("3"));
    }
    [Fact]
    public void Adding_Chore()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);
        // Arrange
        var chores = new List<Chore>
        {
            new Chore { Id = 1, ChoreName = "Chore1", ApartmentId = 1, IsDone = false},
            new Chore { Id = 2, ChoreName = "Chore2", ApartmentId = 1, IsDone = false}
        };

        var mockChoreServices = new Mock<IChoresService>();
        mockChoreServices.Setup(x => x.GetChoresAsync(1)).ReturnsAsync(chores);
        
        mockChoreServices.Setup(x => x.AddChore(It.IsAny<Chore>(), It.IsAny<long>()))
            .Callback<Chore, long>((chore, apartmentId) => chores.Add(chore))
            .ReturnsAsync(1);
    
        Services.AddSingleton(mockChoreServices.Object);

        var component = RenderComponent<AddEditChore>(parameters => parameters.Add(p => p.ApartmentId, 1));
        

        component.Find("#ChoreName").Input("Chore3");
        component.Find("#SubmitButton").Click();
        
        var choreListComponent = RenderComponent<ChoresList>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );
        
        var rows = choreListComponent.FindAll("table tbody tr");
        
        Assert.Equal(3, rows.Count);
        Assert.Contains(rows, row => row.InnerHtml.Contains("Chore1"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("Chore2"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("Chore3"));
    }
    
    [Fact]
    public void Adding_Purchase()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);
        // Arrange
        var purchaseRecords = new List<PurchaseRecord>
        {
            new PurchaseRecord() { Id = 1, ItemName = "Item1", ApartmentId = 1, IsBought = false},
            new PurchaseRecord() { Id = 2, ItemName = "Item2", ApartmentId = 1, IsBought = false}
        };

        var mockChoreServices = new Mock<IPurchaseServices>();
        mockChoreServices.Setup(x => x.GetPurchaseRecords(1)).Returns(purchaseRecords);
        
        mockChoreServices.Setup(x => x.AddPurchase(It.IsAny<PurchaseRecord>()))
            .Callback<PurchaseRecord>((purchaseRecord) => purchaseRecords.Add(purchaseRecord))
            .ReturnsAsync(1);
    
        Services.AddSingleton(mockChoreServices.Object);

        var component = RenderComponent<AddEditPurchase>(parameters => parameters.Add(p => p.ApartmentId, 1));
        

        component.Find("#ItemName").Input("Item3");
        component.Find("#Check").Change(false);
        component.Find("#SubmitButton").Click();
        
        var purchaseRecordList = RenderComponent<PurchasesLists>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );
        
        var rows = purchaseRecordList.FindAll("table tbody tr");
        
        Assert.Equal(3, rows.Count);
        Assert.Contains(rows, row => row.InnerHtml.Contains("Item3"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("Item1"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("Item2"));
    }
    [Fact]
    public void Purchase_Table_Contains_Purchase_Record()
    {
        var roommateServices = new Mock<IRommateServices>();
        Services.AddSingleton(roommateServices.Object);
        var purchaseRecords = new List<PurchaseRecord>
        {
            new PurchaseRecord() { Id = 1, ItemName = "Item1", ApartmentId = 1, IsBought = false},
            new PurchaseRecord() { Id = 2, ItemName = "Item2", ApartmentId = 1, IsBought = false}
        };
        // Arrange
        var services = new Mock<IPurchaseServices>();
        services.Setup(x => x.GetPurchaseRecords(1)).Returns(purchaseRecords);
        Services.AddSingleton(services.Object);
    
        var component = RenderComponent<PurchasesLists>(
            parameters => parameters
                .Add(p => p.ApartmentId, 1)
        );


        component.Find("h3").MarkupMatches("<h3>Purchases</h3>");
        var rows = component.FindAll("table tbody tr");
        
        Assert.Equal(2, rows.Count);

        Assert.Contains(rows, row => row.InnerHtml.Contains("Item2"));
        Assert.Contains(rows, row => row.InnerHtml.Contains("Item1"));
        
    }
    [Fact]
    public void Component_Renders_Correctly()
    {
        // Arrange & Act
        var component = RenderComponent<CreateApartmentJoinApartment>();

        // Assert
        component.Find("h3").MarkupMatches("<h3>Create or join an apartment</h3>");
        component.FindAll("button")[0].MarkupMatches("<button id=\"CreateApartment\" type=\"button\" class=\"btn btn-primary\"  >Create new apartment</button>");
        component.FindAll("button")[1].MarkupMatches("<button id=\"JoinApartment\" type=\"button\" class=\"btn btn-primary\"  >Join apartment</button>");
    }

    [Fact]
    public void Clicking_CreateApartment_Navigates_To_CreateApartment_Page()
    {
        // Arrange
        var mockNavigationManager = Services.GetRequiredService<FakeNavigationManager>();
        var component = RenderComponent<CreateApartmentJoinApartment>();

        // Act
        component.FindAll("button")[0].Click();

        // Assert
        Assert.Equal("http://localhost/createApartment", mockNavigationManager.Uri);
    }

    [Fact]
    public void Clicking_JoinApartment_Navigates_To_JoinApartment_Page()
    {
        // Arrange
        var mockNavigationManager = Services.GetRequiredService<FakeNavigationManager>();
        var component = RenderComponent<CreateApartmentJoinApartment>();

        // Act
        component.FindAll("button")[1].Click();

        // Assert
        Assert.Equal("http://localhost/joinApartment", mockNavigationManager.Uri);
    }
}