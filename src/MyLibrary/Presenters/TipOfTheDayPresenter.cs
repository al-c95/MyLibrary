//MIT License

//Copyright (c) 2022

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE

using System;
using System.Text;
using MyLibrary.Views;

namespace MyLibrary.Presenters
{
    public class TipOfTheDayPresenter
    {
        protected ITipOfTheDay _view;
        protected string[] _tips;

        protected int _index;

        public TipOfTheDayPresenter(ITipOfTheDay view, string[] tips)
        {
            this._view = view;
            this._view.NextClicked += NextClicked;
            this._view.ShowAllClicked += ShowAllClicked;
            this._view.ShowNextButtonEnabled = true;

            this._tips = tips;

            Random rand = new Random();
            int randomTip = rand.Next(0, this._tips.Length);
            this._view.TipsText = (randomTip+1) + ". " + this._tips[randomTip];
            this._index = randomTip;
        }

        public void ShowAllClicked(object sender, EventArgs e)
        {
            if (this._view.ShowNextButtonEnabled)
            {
                StringBuilder tipsText = new StringBuilder();
                int tipNumber = 1;
                foreach (var tip in this._tips)
                {
                    tipsText.AppendLine(tipNumber + ". " + this._tips[tipNumber - 1]);
                    tipsText.AppendLine();

                    tipNumber++;
                }
                this._view.TipsText = tipsText.ToString();

                this._view.ShowAllButtonText = "Show All";
                this._view.ShowNextButtonEnabled = false;
            }
            else
            {
                this._view.TipsText = (this._index + 1) + ". " + this._tips[this._index];

                this._view.ShowAllButtonText = "Show One";
                this._view.ShowNextButtonEnabled = true;
            }
        }

        public void NextClicked(object sender, EventArgs e)
        {
            if (this._index == (this._tips.Length-1))
            {
                this._index = 0;
            }
            else
            {
                this._index++;
            }

            this._view.TipsText = (this._index + 1) + ". " + this._tips[this._index];
        }
    }//class
}
