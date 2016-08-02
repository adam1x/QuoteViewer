﻿using System;
using System.Windows.Forms;

namespace WindowsFormsViewer
{
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DataViewerForm dataViewerForm = new DataViewerForm();
            Application.Run(dataViewerForm);
        }
    }
}
