using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;

namespace PlaywrightTests;

[TestClass]
public class LoginTests : PageTest
{
    private static string _baseUrl = string.Empty;
    private static string _validUsername = string.Empty;
    private static string _validPassword = string.Empty;
    private static string _invalidUsername = string.Empty;
    private static string _invalidPassword = string.Empty;

    [ClassInitialize]
    public static void ClassInitialize(TestContext _)
    {
        var config = new ConfigurationBuilder()
            .AddUserSecrets<LoginTests>()
            .Build();

        _baseUrl = config["TestSettings:BaseUrl"] ?? throw new InvalidOperationException("BaseUrl is not set in user secrets.");
        _validUsername = config["TestSettings:ValidUsername"] ?? throw new InvalidOperationException("ValidUsername is not set in user secrets.");
        _validPassword = config["TestSettings:ValidPassword"] ?? throw new InvalidOperationException("ValidPassword is not set in user secrets.");
        _invalidUsername = config["TestSettings:InvalidUsername"] ?? throw new InvalidOperationException("InvalidUsername is not set in user secrets.");
        _invalidPassword = config["TestSettings:InvalidPassword"] ?? throw new InvalidOperationException("InvalidPassword is not set in user secrets.");
    }

    [TestInitialize]
    public async Task TestInitialize()
    {
        // Grant permissions for camera and microphone
        await Page.Context.GrantPermissionsAsync(new[] { "microphone", "camera" });

        await Page.GotoAsync(_baseUrl);
    }

    [TestMethod]
    public async Task MainNavigation()
    {
        await Expect(Page).ToHaveURLAsync(_baseUrl);
    }

    [TestMethod]
    public async Task TC_001_InvalidUsername()
    {
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync(_invalidUsername);
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync(_validPassword);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        await Expect(Page.GetByText("Invalid Username or Password")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task TC_002_InvalidPassword()
    {
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync(_validUsername);
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync(_invalidPassword);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        await Expect(Page.GetByText("Invalid username or Password")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task TC_003_BothInvalidUsernameAndPassword()
    {
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync(_invalidUsername);
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync(_invalidPassword);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        await Expect(Page.GetByText("Invalid username or Password")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task TC_004_EmptyUsername()
    {
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync("");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync(_validPassword);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        await Expect(Page.GetByText("Enter a Username or Password")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task TC_005_EmptyPassword()
    {
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync(_validUsername);
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync("");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        await Expect(Page.GetByText("Enter a Username or Password")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task TC_006_BothEmptyUsernameAndPassword()
    {
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync("");
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync("");
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        await Expect(Page.GetByText("Enter a Username or Password")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task TC_007_SuccessfulLogin()
    {
        // Use valid credentials from user secrets (assuming Mary404 credentials are valid)
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Username" }).FillAsync(_validUsername);
        await Page.GetByRole(AriaRole.Textbox, new PageGetByRoleOptions { Name = "Password" }).FillAsync(_validPassword);
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "Sign in" }).ClickAsync();
        
        // Handle the dialog and logout
        await Page.GetByRole(AriaRole.Button, new PageGetByRoleOptions { Name = "OK" }).ClickAsync();
        await Page.GetByTitle("Logout").Locator("span").ClickAsync();
        
        // Verify we're back at login screen
        await Expect(Page.Locator(".Login-background")).ToBeVisibleAsync();
    }
}