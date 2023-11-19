using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace laba8
{
    public partial class MainWindow : Window
    {
        private string connectionString = "Data Source=DESKTOP-HJDSDO1;Initial Catalog=AthletesDB;Integrated Security=True";
        private ObservableCollection<Athlete> athletes;

        public MainWindow()
        {
            InitializeComponent();
            athletes = new ObservableCollection<Athlete>();
            athletesListView.ItemsSource = athletes;
            LoadAthletes();

            // Додаємо подію SelectionChanged для обробки вибору елемента в ListView
            athletesListView.SelectionChanged += AthletesListView_SelectionChanged;
        }

        private void AthletesListView_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Перевіряємо, чи є обраний елемент
            if (athletesListView.SelectedIndex != -1)
            {
                // Заповнюємо текстові поля даними обраного спортсмена
                Athlete selectedAthlete = athletes[athletesListView.SelectedIndex];
                codeTextBox.Text = selectedAthlete.Code;
                lastNameTextBox.Text = selectedAthlete.LastName;
                firstNameTextBox.Text = selectedAthlete.FirstName;
                middleNameTextBox.Text = selectedAthlete.MiddleName;
                birthYearTextBox.Text = selectedAthlete.BirthYear;
                genderTextBox.Text = selectedAthlete.Gender;
                sportTypeTextBox.Text = selectedAthlete.SportType;
                achievementsTextBox.Text = selectedAthlete.Achievements;
                medalTypeTextBox.Text = selectedAthlete.MedalType;
            }
        }

        private void LoadAthletes()
        {
            athletes.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Athletes ORDER BY Code";
                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    athletes.Add(new Athlete
                    {
                        Code = reader["Code"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        FirstName = reader["FirstName"].ToString(),
                        MiddleName = reader["MiddleName"].ToString(),
                        BirthYear = reader["BirthYear"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        SportType = reader["SportType"].ToString(),
                        Achievements = reader["Achievements"].ToString(),
                        MedalType = reader["MedalType"].ToString()
                    });
                }
                reader.Close();
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO Athletes (Code, LastName, FirstName, MiddleName, BirthYear, Gender, SportType, Achievements, MedalType) " +
                               "VALUES (@Code, @LastName, @FirstName, @MiddleName, @BirthYear, @Gender, @SportType, @Achievements, @MedalType)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@Code", codeTextBox.Text);
                command.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
                command.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
                command.Parameters.AddWithValue("@MiddleName", middleNameTextBox.Text);
                command.Parameters.AddWithValue("@BirthYear", birthYearTextBox.Text);
                command.Parameters.AddWithValue("@Gender", genderTextBox.Text);
                command.Parameters.AddWithValue("@SportType", sportTypeTextBox.Text);
                command.Parameters.AddWithValue("@Achievements", achievementsTextBox.Text);
                command.Parameters.AddWithValue("@MedalType", medalTypeTextBox.Text);
                command.ExecuteNonQuery();
            }
            ClearTextBoxes();
            LoadAthletes();
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (athletesListView.SelectedIndex != -1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Athletes SET LastName = @LastName, FirstName = @FirstName, " +
                                   "MiddleName = @MiddleName, BirthYear = @BirthYear, Gender = @Gender, " +
                                   "SportType = @SportType, Achievements = @Achievements, MedalType = @MedalType " +
                                   "WHERE Code = @Code";

                    SqlCommand command = new SqlCommand(query, connection);

                    command.Parameters.AddWithValue("@Code", athletes[athletesListView.SelectedIndex].Code);
                    command.Parameters.AddWithValue("@LastName", lastNameTextBox.Text);
                    command.Parameters.AddWithValue("@FirstName", firstNameTextBox.Text);
                    command.Parameters.AddWithValue("@MiddleName", middleNameTextBox.Text);
                    command.Parameters.AddWithValue("@BirthYear", birthYearTextBox.Text);
                    command.Parameters.AddWithValue("@Gender", genderTextBox.Text);
                    command.Parameters.AddWithValue("@SportType", sportTypeTextBox.Text);
                    command.Parameters.AddWithValue("@Achievements", achievementsTextBox.Text);
                    command.Parameters.AddWithValue("@MedalType", medalTypeTextBox.Text);

                    command.ExecuteNonQuery();
                }
                ClearTextBoxes();
                LoadAthletes();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (athletesListView.SelectedIndex != -1)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Athletes WHERE Code = @Code";
                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Code", athletes[athletesListView.SelectedIndex].Code);

                    command.ExecuteNonQuery();
                }
                ClearTextBoxes();
                LoadAthletes();
            }
        }
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBoxes();
        }

        private void ClearTextBoxes()
        {
            codeTextBox.Text = "";
            lastNameTextBox.Text = "";
            firstNameTextBox.Text = "";
            middleNameTextBox.Text = "";
            birthYearTextBox.Text = "";
            genderTextBox.Text = "";
            sportTypeTextBox.Text = "";
            achievementsTextBox.Text = "";
            medalTypeTextBox.Text = "";
        }
    }

    public class Athlete
    {
        public string Code { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string BirthYear { get; set; }
        public string Gender { get; set; }
        public string SportType { get; set; }
        public string Achievements { get; set; }
        public string MedalType { get; set; }
    }
}