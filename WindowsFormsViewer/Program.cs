﻿using System;
using System.Windows.Forms;

namespace WindowsFormsViewer
{
    public static class Program
    {
        internal static Manager FormsManager = new Manager();

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DataViewerForm());
        }
    }
}
