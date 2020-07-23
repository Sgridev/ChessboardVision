using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChessboardVision
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InfoPage : ContentPage
    {
        public InfoPage()
        {
            InitializeComponent();


            TapGestureRecognizer sgriTap = new TapGestureRecognizer();
            sgriTap.Tapped += async (sender, e) => {
                await Browser.OpenAsync("https://andreagrisafi.it/", BrowserLaunchMode.SystemPreferred);
            };
            sgri.GestureRecognizers.Add(sgriTap);
        }
    }
}