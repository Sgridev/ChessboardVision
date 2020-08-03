using ChessboardVision.Model;
using ChessboardVision.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace ChessboardVision.Views
{

    public partial class GamePage : ContentPage
    {
        readonly GameViewModel viewModel;
        public GamePage(bool timePressure)
        {
            InitializeComponent();
            viewModel = new GameViewModel(timePressure, Navigation);
            viewModel.PropertyChanged += OnViewModelPropertyChanged;
            BindingContext = viewModel;
        }

        private async void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLightPressedCorrect":
                    await btnLight.ChangeBackgroundColorTo(Color.Green, 250, Easing.CubicOut);
                    await btnLight.ChangeBackgroundColorTo(Color.White, 250, Easing.CubicOut);
                    break;
                case "IsDarkPressedCorrect":
                    await btnDark.ChangeBackgroundColorTo(Color.Green, 250, Easing.CubicOut);
                    await btnDark.ChangeBackgroundColorTo(Color.Black, 250, Easing.CubicOut);
                    break;
                case "IsLightPressedWrong":
                    await btnLight.ChangeBackgroundColorTo(Color.Red, 250, Easing.CubicOut);
                    await btnLight.ChangeBackgroundColorTo(Color.White, 250, Easing.CubicOut);
                    break;
                case "IsDarkPressedWrong":
                    await btnDark.ChangeBackgroundColorTo(Color.Red, 250, Easing.CubicOut);
                    await btnDark.ChangeBackgroundColorTo(Color.Black, 250, Easing.CubicOut);
                    break;
                default:
                    break;

            }
        }

        protected async override void OnAppearing()
        {
            await progressBar.ProgressTo(1, 60000, Easing.Linear);

        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert", "Do you really want to go back to the main menu?", "Yes", "No");
                if (result)
                    if (viewModel.BackPressCommand.CanExecute(true))
                        viewModel.BackPressCommand.Execute(true);
                    
            });

            return true;
        }
    }
}