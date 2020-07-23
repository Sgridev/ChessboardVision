using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChessboardVision
{
   
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private bool _isRunning = false;
        public MainPage()
        {
            InitializeComponent();
            TapGestureRecognizer infoTap = new TapGestureRecognizer();
            infoTap.Tapped += async (sender, e) => {
                await Navigation.PushAsync(new InfoPage());
            };
            infoImage.GestureRecognizers.Add(infoTap);

            TapGestureRecognizer profileTap = new TapGestureRecognizer();
            profileTap.Tapped += async (sender, e) => {
                await Navigation.PushAsync(new ProfilePage());
            };
            profileImage.GestureRecognizers.Add(profileTap);
        }

        private async void StandardButton_OnClicked(object sender, EventArgs e)
        {
                if (_isRunning)
                    return;
                else
                    _isRunning = true;
          
            await Navigation.PushAsync(new GamePage());
            _isRunning = false;
        }
        private async void TimeButton_OnClicked(object sender, EventArgs e)
        {
            if (_isRunning)
                return;
            else
                _isRunning = true;
            await Navigation.PushAsync(new GamePage(true));
            _isRunning = false;
        }


    }
}
