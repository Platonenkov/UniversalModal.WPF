# UniversalModal.WPF

Modal window for WPF
implement IEnumerable container

### Installation:
Install-Package UniversalModal.WPF -Version 1.0.0.2

## Usage:
add to view this code
```XAML
        xmlns:views="clr-namespace:UniversalModal.WPF.Views;assembly=UniversalModal.WPF"
        xmlns:interfaces="clr-namespace:UniversalModal.WPF.Interfaces;assembly=UniversalModal.WPF"
```
### Base modal Implementation

for use modal window you need implement you model from IModalContainer, INotifyPropertyChanged
base realization is ModalContainer
```C#
        private void TestModal(object Sender, RoutedEventArgs E)
        {
            var model = new ModalContainer(){ModalBrush = new SolidColorBrush(Color.FromArgb(200, 169, 169, 169))};
            var win = new ModalTestWindow() {DataContext = model};
            win.Show();
        }
```
```XAML
        <views:UniversalModalView Controller="{Binding}" ShowCloseButton="True" ShowOpenButton="True">
            <views:UniversalModalView.MainContent>
                <StackPanel Orientation="Horizontal" VerticalAlignment="Center" HorizontalAlignment="Center">
                    <TextBlock Text="Main data information. Press top and right button to open modal" HorizontalAlignment="Center" VerticalAlignment="Center"/>
                </StackPanel>
            </views:UniversalModalView.MainContent>
            <views:UniversalModalView.ModalContent>
                <Grid>
                    <TextBlock Text="Some information or content. What you want." HorizontalAlignment="Center" VerticalAlignment="Top"/>
                    <TextBlock Text="Some information or content. What you want." HorizontalAlignment="Center" VerticalAlignment="Bottom"/>
                </Grid>
            </views:UniversalModalView.ModalContent>
        </views:UniversalModalView>
```

![Demo](https://github.com/Platonenkov/UniversalModal.WPF/blob/master/Resources/Modal.gif)

### Base IEnumerable viewer

```C#
    var data = new List<int>();
    for (var i = 1; i < 10; i++)
        data.Add(i);

    var model = new EnumerableUniversalModalViewModel<int>(data,true, new SolidColorBrush(Color.FromArgb(200, 169, 169, 169)));

    var win = new EnumerableTestWindow(){DataContext = model};
    win.Show();
```
```XAML
    <views:EnumerableUniversalModalContainerView DataContext="{Binding}"/>
```
![Demo](https://github.com/Platonenkov/UniversalModal.WPF/blob/master/Resources/Enumerable.gif)

### Named container

implement you class from INamedElement

```C#
    public class NamedTestClass : INamedElement
    {

        #region Implementation of INamedElement

        private string _Name;
        public string Name
        {
            get => _Name;
            set
            {
                _Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public bool Rename(string NewName)
        {
            Name = NewName; //тут проверка возможно ли переименование или нет
            return true;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion

        public string Description { get; set; }
    }
```

```C#
      var data = new List<NamedTestClass>();
      for (var i = 0; i < 10; i++)
      data.Add(new NamedTestClass(){Description = $"Test model {rnd.Next(1,100)}", Name = $"Element {rnd.Next(1,100)}"});

      var model = new UniversalModalViewModelNamed<NamedTestClass>(data, true, new SolidColorBrush(Color.FromArgb(200, 169, 169, 169)));
      var win = new NamedTestWindow(){DataContext = model};
      win.Show();
```

```XAML
    <views:UniversalModalContainerNamedView ShowAddButton="False" DataContext="{Binding }" Margin="3">
        <DockPanel>
            <DataGrid ItemsSource="{Binding Elements, UpdateSourceTrigger=PropertyChanged}"
                      AutoGenerateColumns="False"
                      VirtualizingPanel.ScrollUnit="Pixel" CanUserAddRows="False" CanUserDeleteRows="False"
                      >
                <DataGrid.Columns>
                    <DataGridTemplateColumn Width="40" MinWidth="50" >
                        <DataGridTemplateColumn.Header>
                            <Button Style="{StaticResource OverButton}" ToolTip="Remove" 
                                        VerticalAlignment="Center" HorizontalAlignment="Center"
                                        Command="{Binding DataContext.OpenModalCommand,
                                                        RelativeSource={RelativeSource AncestorType=Window}}"
                                        CommandParameter="{Binding}">
                                <TextBlock Text="&#xE0C5;" FontFamily="Segoe UI Symbol" FontWeight="Bold" FontSize="8"
                                                   HorizontalAlignment="Center" Foreground="Green">
                                </TextBlock>
                            </Button>
                        </DataGridTemplateColumn.Header>
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate DataType="{x:Type base:NamedTestClass}">
                                <StackPanel Orientation="Horizontal">
                                    <Button Style="{StaticResource OverButton}" ToolTip="Remove" 
                                        VerticalAlignment="Center" HorizontalAlignment="Center"
                                        Command="{Binding DataContext.RemoveElementCommand,
                                                        RelativeSource={RelativeSource AncestorType=Window}}"
                                        CommandParameter="{Binding}">
                                        <TextBlock Text="&#xE0D9;" FontFamily="Segoe UI Symbol" FontWeight="Bold"
                                                   HorizontalAlignment="Center" Foreground="Orange">
                                        </TextBlock>
                                    </Button>
                                    <Button Style="{StaticResource OverButton}" ToolTip="Rename" 
                                            VerticalAlignment="Center" HorizontalAlignment="Right"
                                            Command="{Binding DataContext.RenameElementCommand, 
                                                        RelativeSource={RelativeSource AncestorType=Window}}"
                                            CommandParameter="{Binding SelectedItem,RelativeSource={RelativeSource AncestorType={x:Type DataGrid}}}"
                                            DockPanel.Dock="Right">
                                        <TextBlock Text="&#xE0D8;" FontFamily="Segoe UI Symbol" FontWeight="Bold"
                                                   HorizontalAlignment="Center" Foreground="Orange">
                                        </TextBlock>
                                    </Button>

                                </StackPanel>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>
                    </DataGridTemplateColumn>
                    <DataGridTemplateColumn>
                        <DataGridTemplateColumn.Header>
                            <DockPanel>
                                <TextBlock Text="Name" />
                            </DockPanel>
                        </DataGridTemplateColumn.Header>
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate DataType="{x:Type base:NamedTestClass}">
                                <TextBlock Text="{Binding Name,UpdateSourceTrigger=PropertyChanged}" Margin="3,0" VerticalAlignment="Center"/>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>
                    </DataGridTemplateColumn>
                    <DataGridTemplateColumn Width="*">
                        <DataGridTemplateColumn.Header>
                            <DockPanel>
                                <TextBlock Text="Description" />
                            </DockPanel>
                        </DataGridTemplateColumn.Header>
                        <DataGridTemplateColumn.CellTemplate>
                            <DataTemplate DataType="{x:Type base:NamedTestClass}">
                                <TextBlock Text="{Binding Description,UpdateSourceTrigger=PropertyChanged}" Margin="3,0" VerticalAlignment="Center"/>
                            </DataTemplate>
                        </DataGridTemplateColumn.CellTemplate>
                    </DataGridTemplateColumn>
                </DataGrid.Columns>

            </DataGrid>
        </DockPanel>
    </views:UniversalModalContainerNamedView>
```
![Demo](https://github.com/Platonenkov/UniversalModal.WPF/blob/master/Resources/Named.gif)
