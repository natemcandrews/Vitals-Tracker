﻿using CalendarSolution.Controls;
using OutlookCalendar.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace CalendarSolution
{
    /// <summary>
    /// Interaction logic for AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        private Appointments appointments = new Appointments();
        string path = ((System.IO.Path.GetPathRoot(Environment.SystemDirectory) + "/Patient Forms/"));

        public AppointmentWindow()
        {
            InitializeComponent();
            retrieve();
            DataContext = appointments;
        }



        private void Calendar_AddAppointment(object sender, RoutedEventArgs e)
        {
            Appointment appointment = new Appointment();
            appointment.Subject = "Subject?";
            appointment.StartTime = DateProperty.ScheduleDate;
            appointment.EndTime = DateProperty.ScheduleDate;

            AddAppointmentWindow aaw = new AddAppointmentWindow();
            aaw.DataContext = appointment;
            aaw.ShowDialog();

            appointments.Add(appointment);
            save();
        }

        private void retrieve()
        {
            String path = (System.IO.Path.GetPathRoot(Environment.SystemDirectory) + "/Patient Forms/");
            if (File.Exists(path + "Appointments/appointmentList.json")) //Checks if the patient data already exists
            {
                Type type = typeof(Appointments);
                XmlSerializer serializer = new XmlSerializer(type); //Serializes data if it exists
                StreamReader reader = File.OpenText(path + "Appointments/appointmentList.json");
                Appointments temp = (Appointments)serializer.Deserialize(reader);
                foreach (Appointment app in temp)
                {
                    appointments.Add(app);
                }
                reader.Close();
            }
        }

        private void save()
        {
            try
            {
                XmlSerializer Fullserializer = new XmlSerializer(appointments.GetType()); //Saves all data to the specified folder
                StreamWriter Fullwriter = File.CreateText(path + "Appointments/appointmentList.json"); //The folder location
                Fullserializer.Serialize(Fullwriter.BaseStream, appointments);
                Fullwriter.Close();
            }
            catch (IOException e)
            {
                Console.Write(e.Message);
            }

        }
    }
}
