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
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            best_score_standard.Text = Preferences.Get("best_score_standard", 0).ToString();
            best_score_time_pressure.Text = Preferences.Get("best_score_time_pressure", 0).ToString();
            games_played.Text = Preferences.Get("games_played", 0).ToString();
            correct_answers.Text = Preferences.Get("correct_answers", 0).ToString();
            wrong_answers.Text = Preferences.Get("wrong_answers", 0).ToString();


        }
    }
}