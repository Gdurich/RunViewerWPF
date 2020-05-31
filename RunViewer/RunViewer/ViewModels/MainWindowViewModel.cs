using Microsoft.Win32;
using RunViewer.Helpers;
using RunViewer.Models;
using System;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RunViewer.ViewModels
{
    public class MainWindowViewModel: Base.ViewModel
    {
        private readonly IContext _context;
        public ObservableCollection<ProgramItem> ProgramItems { get; set; }
        public MainWindowViewModel()
        {
            _context = new WpfDispatcherContext();
            ProgramItems = new ObservableCollection<ProgramItem>();
        }
        void LoadItemsFromRegistry()
        {
            string pathRun = "";
            if (Environment.Is64BitOperatingSystem)
                pathRun = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
            else
                pathRun = "SOFTWARE\\Wow6432Node\\Microsoft\\Windows\\CurrentVersion\\Run";
            RegistryKey run = Registry.LocalMachine.OpenSubKey(pathRun);
            foreach (var name in run.GetValueNames())
            {
                string value = run.GetValue(name).ToString().Replace('\"', ' ');
                string path = value.Substring(0, value.LastIndexOf('\\') + 1);
                string commands = value.Substring(value.IndexOf(".exe") + 4);
                Icon icon = Icon.ExtractAssociatedIcon(value.Substring(0, value.IndexOf(".exe") + 4));
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                ProgramItem programItem = new ProgramItem
                {
                    Icon = imageSource,
                    FilePath = path,
                    FileName = name,
                    CommandString = commands,
                    type = "Registry"
                };
                ProgramItems.Add(programItem);
            }
            run = Registry.CurrentUser.OpenSubKey(pathRun);
            foreach (var name in run.GetValueNames())
            {
                string value = run.GetValue(name).ToString().Replace('\"', ' ');
                string path = value.Substring(0, value.LastIndexOf('\\') + 1);
                string commands = value.Substring(value.IndexOf(".exe") + 4);
                Icon icon = Icon.ExtractAssociatedIcon(value.Substring(0, value.IndexOf(".exe") + 4));
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                ProgramItem programItem = new ProgramItem
                {
                    Icon = imageSource,
                    FilePath = path,
                    FileName = name,
                    CommandString = commands,
                    type = "Registry"
                };
                ProgramItems.Add(programItem);
            }
        }
        void LoadItemsFromStartMenu()
        {
            string UnifyPath = "Microsoft\\Windows\\Start Menu\\Programs\\Startup";
            foreach (var file in Directory.GetFiles("C:\\ProgramData\\" + UnifyPath))
            {
                Icon icon = Icon.ExtractAssociatedIcon(file);
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                FileInfo fileInfo = new FileInfo(file);
                ProgramItems.Add(
                    new ProgramItem
                    {
                        Icon = imageSource,
                        FilePath = fileInfo.DirectoryName,
                        FileName = fileInfo.Name,
                        type = "Start Menu"
                    }
                    );
            }
            string UserName = Environment.GetEnvironmentVariable("USERNAME");
            foreach (var file in Directory.GetFiles("C:\\Users\\" + UserName + "\\AppData\\Roaming\\" + UnifyPath))
            {
                Icon icon = Icon.ExtractAssociatedIcon(file);
                ImageSource imageSource = Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                FileInfo fileInfo = new FileInfo(file);
                ProgramItems.Add(
                    new ProgramItem
                    {
                        Icon = imageSource,
                        FilePath = fileInfo.DirectoryName,
                        FileName = fileInfo.Name,
                        type = "Start Menu"
                    }
                    );
            }
        }
        public async Task LoadItems()
        {           
            await Task.Run(() =>
            {
                //я не вижу без задержки строку загрузки
                Thread.Sleep(5000);
                _context.Invoke(() => {
                    LoadItemsFromRegistry();
                    LoadItemsFromStartMenu();
                });
            });
        }
    }
}
