using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using NominateAndVote.DataModel.Tests;
using NominateAndVote.DataTableStorage;
using NominateAndVote.DataTableStorage.Entity;
using System.Threading;
using System.Windows;

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