using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using NominateAndVote.DataModel.Poco;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage;
using NominateAndVote.DataTableStorage.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.MessageBox;

namespace NominateAndVote.DataTableStorageHelper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Load_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("It should be quite fast");

            // create data manager
            var dataManager = CreateDataManager();
            dataManager.CreateTablesIfNeeded();

            // add data
            var dataModel = new SampleDataModel();
            foreach (var poco in dataModel.Administrators)
            {
                dataManager.SaveEntity(new AdministratorEntity(poco));
            }
            foreach (var poco in dataModel.News)
            {
                dataManager.SaveEntity(new NewsEntity(poco));
            }
            foreach (var poco in dataModel.Nominations)
            {
                dataManager.SaveEntity(new NominationEntity(poco));
            }
            foreach (var poco in dataModel.Polls)
            {
                dataManager.SaveEntity(new PollEntity(poco));
            }
            // skip poll subjects
            foreach (var poco in dataModel.Users)
            {
                dataManager.SaveEntity(new UserEntity(poco));
            }
            foreach (var poco in dataModel.Votes)
            {
                dataManager.SaveEntity(new VoteEntity(poco));
            }
            MessageBox.Show("Done!");
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "It can take even one minute! Be not afraid if the GUI freezes (sorry, it is only a helper app)");

            // create data manager
            var dataManager = CreateDataManager();

            // delete tables
            foreach (var entityType in TableNames.GetEntityTypes())
            {
                if (entityType != typeof(PollSubjectEntity))
                {
                    dataManager.GetTableReference(entityType).Delete();
                }
            }

            Thread.Sleep(60000);

            // create tables
            dataManager.CreateTablesIfNeeded();

            MessageBox.Show("Done!");
        }

        private void Button_Upload_Click(object sender, RoutedEventArgs e)
        {
            // browse file
            var fileDialog = new OpenFileDialog();

            // optional filter to restrict file types
            fileDialog.Filter = "CSV Files|*.csv";

            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK)
            {
                return;
            }

            var csv = new CsvReader(new StreamReader(fileDialog.FileName));

            var pollSubjects = new List<PollSubject>();
            while (csv.Read())
            {
                var ps = new PollSubject
                {
                    Id = csv.GetField<long>(0),
                    Title = csv.GetField<string>(2),
                    Year = csv.GetField<int>(3)
                };
                pollSubjects.Add(ps);
            }

            MessageBox.Show("Read " + pollSubjects.Count + " poll subjects, saving them will take a while");
            MessageBox.Show("Make sure you have no poll subjects in the table before continuing");

            // create data manager
            var dataManager = CreateDataManager();
            dataManager.CreateTablesIfNeeded();

            // save poll subjects
            var total = pollSubjects.Count;
            var remaining = total;

            // tactic: remove saved items from list
            while (remaining > 0)
            {
                var n = Math.Min(500, remaining);
                var somePs = pollSubjects.GetRange(0, n);

                dataManager.SavePollSubjectsBatch(somePs);

                pollSubjects.RemoveRange(0, n);
                remaining -= n;
            }
        }

        private TableStorageDataManager CreateDataManager()
        {
            // set table names
            TableNames.ResetToDefault();

            // connect and create tables
            var storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            return new TableStorageDataManager(storageAccount);
        }
    }
}