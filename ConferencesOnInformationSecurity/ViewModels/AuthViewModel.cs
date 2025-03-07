using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using Avalonia.Threading;
using ConferencesOnInformationSecurity.Models;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Tmds.DBus.Protocol;

namespace ConferencesOnInformationSecurity.ViewModels
{
	public class AuthViewModel : ViewModelBase
	{
		string email;
		string password;
        string message;
        
        bool isVisible = true;
        string capcha;
        string capchaInput;
        Canvas canvasCapcha;
        int counter = 0;

        DispatcherTimer _timer;

        public string Email { get => email; set => this.RaiseAndSetIfChanged(ref email, value); }
        public string Password { get => password; set => this.RaiseAndSetIfChanged(ref password, value); }
        public string Message { get => message; set => this.RaiseAndSetIfChanged(ref message, value); }
        public bool IsVisible { get => isVisible; set => this.RaiseAndSetIfChanged(ref isVisible, value); }
        public string Capcha { get => capcha; set => this.RaiseAndSetIfChanged(ref capcha, value); }
        public string CapchaInput { get => capchaInput; set => this.RaiseAndSetIfChanged(ref capchaInput, value); }
        public Canvas CanvasCapcha { get => canvasCapcha; set => this.RaiseAndSetIfChanged(ref canvasCapcha, value); }


        public AuthViewModel()
        {
            GenerateCapcha();
        }

        public void Enter()
		{
            Message = "";
            if(Email == "" || Password == "" || CapchaInput=="")
            {
                counter++;
                Message = "Поля не заполнены";
                GenerateCapcha();
                return;
            }

            if(CapchaInput != Capcha)
            {
                Message = "Капча не совпадает";
                CapchaInput = "";
                GenerateCapcha();
                return;
            }


            Client? client = db.Clients.FirstOrDefault(x => x.Email == Email && x.Password == Password);
            Organizer organizer = db.Organizers.Include(x => x.Gender).FirstOrDefault(x => x.Email == Email && x.Password == Password);
            Moder? moder = db.Moders.Include(x => x.Gender).FirstOrDefault(x => x.Email == Email && x.Password == Password);
            Jury? jury = db.Juries.Include(x => x.Gender).FirstOrDefault(x => x.Email == Email && x.Password == Password);
            if (client != null || organizer != null || moder != null || jury != null)
            {
                if (client != null)
                {
                    MainWindowViewModel.Self.Uc = new ClientPage();
                    return;
                }
                if (organizer != null)
                {
                    MainWindowViewModel.Self.AuthOrganizer = organizer;
                    MainWindowViewModel.Self.Uc = new OrganizerPage();
                    return;
                }


                if (moder != null)
                {
                    MainWindowViewModel.Self.Uc = new ModersPage();
                    return;
                }


                if (jury != null)
                {
                    MainWindowViewModel.Self.Uc = new JuryPage();
                    return;
                }
            }

            else
            {
                counter++;
                if (counter == 3)
                {
                    Message = "Разблокировка системы через 15 секунд";
                    IsVisible = false;
                    _timer.Interval = new TimeSpan(0, 0, 15);
                    _timer.Tick += TimerTick;
                    _timer.Start();
                }
                else
                {
                    Message = "Неверные лд";
                }
            }
            GenerateCapcha();
        }

        private void TimerTick(object send, EventArgs e)
        {
            IsVisible = true;
            counter = 0;
        }

        public void GenerateCapcha()
        {
            Random rand = new Random();
            const string symbols = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int lenth = 4;
            Capcha = "";
            Canvas canvas = new Canvas()
            {
                Width = 200,
                Height = 90,
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
                Background = new SolidColorBrush(Colors.White)
            };
            double startX = rand.Next(10, 20);
            for (int i = 0; i < lenth; i++)
            {
                TextBlock myTextBlock = new TextBlock();
                myTextBlock.FontSize = 20;
                myTextBlock.HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center;
                myTextBlock.VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center;
                myTextBlock.Foreground = new SolidColorBrush(Colors.Black);
                myTextBlock.FontWeight = FontWeight.Bold;

                int index = rand.Next(symbols.Length);
                char result = symbols[index];
                myTextBlock.Text = result.ToString();
                Capcha += Convert.ToString(result);

                Canvas.SetLeft(myTextBlock, startX + (i * 30));
                Canvas.SetTop(myTextBlock, rand.Next(5, 40));
                canvas.Children.Add(myTextBlock);
            }

            for (int i = 0; i < rand.Next(15, 40); i++)
            {
                Line line = new Line()
                {
                    StartPoint = new Avalonia.Point(rand.Next(200), rand.Next(90)),
                    EndPoint = new Avalonia.Point(rand.Next(200), rand.Next(90)),
                    Stroke = new SolidColorBrush(Colors.Aqua),

                    StrokeThickness = rand.Next(3)
                };
                canvas.Children.Add(line);
            }

            CanvasCapcha = canvas;
        }


        public void Back()
        {
            MainWindowViewModel.Self.Uc = new SharedView();
        }

    }
}