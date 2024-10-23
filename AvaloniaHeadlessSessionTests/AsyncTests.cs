using Avalonia.Controls;

namespace AvaloniaHeadlessSessionTests;

// Example test class with async tests
public class AsyncTests : AvaloniaHeadlessTestBase
{
    [Fact]
    public async Task TextBox_AsyncInput_ShouldUpdate()
    {
        await RunInHeadlessSession(async () =>
        {
            // Arrange
            var textBox = new TextBox();
            var window = new Window { Content = textBox };
            window.Show();

            // Act
            textBox.Text = "Initial";
            await Task.Delay(50); // Simulate some async operation
            textBox.Text = "Updated";

            // Assert
            Assert.Equal("Updated", textBox.Text);

            // Cleanup
            window.Close();
        });
    }

    [Theory]
    [InlineData("Test 1")]
    [InlineData("Test 2")]
    public async Task TextBox_MultipleInputs_ShouldUpdate(string input)
    {
        var result = await RunInHeadlessSession(async () =>
        {
            // Arrange
            var textBox = new TextBox();
            var window = new Window { Content = textBox };
            window.Show();

            // Act
            textBox.Text = input;
            await Task.Delay(50); // Simulate some async operation

            // Cleanup
            window.Close();
            return textBox.Text;
        });

        // Assert
        Assert.Equal(input, result);
    }
}
