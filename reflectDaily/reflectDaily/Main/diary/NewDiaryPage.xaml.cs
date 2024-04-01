using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace reflectDaily.Main.diary
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewDiaryPage : ContentPage
	{
		public NewDiaryPage ()
		{
			InitializeComponent ();
		}
	}
}