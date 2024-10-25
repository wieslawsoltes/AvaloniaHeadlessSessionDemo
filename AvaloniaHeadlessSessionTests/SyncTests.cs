using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AvaloniaHeadlessSessionTests;

// Example test class with synchronous tests
public class SyncTests : AvaloniaHeadlessTestBase
{
    [Fact]
    public void Button_Click_ShouldTriggerEvent()
    {
        RunInHeadlessSession(() =>
        {
            // Arrange
            var clicked = false;
            var button = new Button { Content = "Test" };
            var window = new Window { Content = button };
            button.Click += (_, _) => clicked = true;
            window.Show();

            // Act
            button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));

            // Assert
            Assert.True(clicked);

            // Cleanup
            window.Close();
        });
    }

    [Fact]
    public void TextBox_SetText_ShouldUpdateImmediately()
    {
        RunInHeadlessSession(() =>
        {
            // Arrange
            var textBox = new TextBox();
            var window = new Window { Content = textBox };
            window.Show();

            // Act
            textBox.Text = "Test Value";

            // Assert
            Assert.Equal("Test Value", textBox.Text);

            // Cleanup
            window.Close();
        });
    }

    [Fact]
    public async Task Window_Size_ShouldBeSet()
    {
        var size = await RunInHeadlessSession(() =>
        {
            // Arrange
            var window = new Window
            {
                Width = 800,
                Height = 600
            };
            window.Show();

            // Act
            var size = new Size(window.Width, window.Height);

            // Cleanup
            window.Close();

            return size;
        });

        // Assert
        Assert.Equal(800, size.Width);
        Assert.Equal(600, size.Height);
    }
}
