using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace RGR_Shevchuk
{
    internal class ViewClass
    {
        public void MainMenu()
        {
            Console.WriteLine(" ___Hospital Database__");
            Console.WriteLine("|        Tables:       |");
            Console.WriteLine(" ----------------------");
            Console.WriteLine("| 1. Doctor            |");
            Console.WriteLine("| 2. Hospital          |");
            Console.WriteLine("| 3. Patient           |");
            Console.WriteLine("| 4. Patient_Card      |");
            Console.WriteLine("| 5. Patient_Doctor    |");
            Console.WriteLine(" ----------------------");
            Console.WriteLine("| Escape - finish      |");
            Console.WriteLine(" ----------------------");
        }
        public void Clear()
        {
            System.Console.Clear();
        }
        public void Pause()
        {
            Console.ReadKey();
        }
        public void Output(string s)
        {
            Console.WriteLine("--" + s + "--");
        }


        public void TMenu()
        {
            Console.WriteLine(" - Choose action:       ");
            Console.WriteLine(" - 1. View all          ");
            Console.WriteLine(" - 2. Input new raw     ");
            Console.WriteLine(" - 3. Remove row        ");
            Console.WriteLine(" - 4. Edit existing row ");
            Console.WriteLine(" - 5. Serch row         ");
            Console.WriteLine(" - 6. Generate data     ");
        }
        public void AllDoctors(List<TDoctor> doctors)
        {
            Console.WriteLine(" ----Doctor:-----");
            foreach (TDoctor doctor in doctors)
            {
                Console.WriteLine("|{0,3}|{1,25}|{2,25}|№{3,3}|", doctor.Id, doctor.Name, doctor.Profile, doctor.Hospital);
            }
        }
        public TDoctor InputDoctor() 
        {
            TDoctor dct = new TDoctor();
            Console.WriteLine(" ----Input:-----");
            Console.WriteLine("Input data about doctor:");
            Console.Write("Name        : ");
            dct.Name = Console.ReadLine();
            Console.Write("Profile     : ");
            dct.Profile = Console.ReadLine();
            Console.Write("Hospital Id : ");
            dct.Hospital = Convert.ToInt32(Console.ReadLine());

            return dct;
        }
        public int ChooseId()
        {
            Console.WriteLine("\nChoose ID:");
            int i = Convert.ToInt32(Console.ReadLine());
            return i;
        }
        public void SearchDoctor()
        {
            Console.WriteLine("\nChoose column of search:");
            Console.WriteLine("  - 1. Name      ");
            Console.WriteLine("  - 2. Profile   ");
            Console.WriteLine("  - 3. Hospital  ");
        }
        public string SearchName()
        {
            Console.WriteLine("\nInput name:");
            string str = Console.ReadLine();
            return str;
        }
        public string SearchProfile()
        {
            Console.WriteLine("\nInput profile:");
            string str = Console.ReadLine();
            return str;
        }
        public int SearchDHospital()
        {
            Console.WriteLine("\nInput number of hospital:");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        public int Generate()
        {
            Console.WriteLine("\nInput number of raws that you want to generate:");
            int n = Convert.ToInt32(Console.ReadLine());
            return n;
        }
       
//// // // // // // // // // // // /// // // // /// ///
// // // // // // // // // // // /// // // // /// /// 

        public void AllHospitals(List<THospital> hospitals)
        {
            Console.WriteLine(" ----Hospital:-----");

            Console.WriteLine("|{0,3}|{1,30}|{2,5}|","Id", "Address", "Count");
            Console.WriteLine(" -----------------------------------------");
            foreach (THospital h in hospitals)
            {
                Console.WriteLine("|{0,3}|{1,30}|{2,5}|", h.Id, h.Address, h.Number);
            }
        }
        public THospital InputHospital()
        {
            THospital hsp = new THospital();
            Console.WriteLine(" ----Input:-----");
            Console.WriteLine("Input data about hospital:");
            Console.Write("Address            : ");
            hsp.Address = Console.ReadLine();
            Console.Write("Number of patients : ");
            hsp.Number = Convert.ToInt32(Console.ReadLine());

            return hsp;
        }
        public void SearchHospital()
        {
            Console.WriteLine("\nChoose column of search:");
            Console.WriteLine("  - 1. Address      ");
            Console.WriteLine("  - 2. Number of patients   ");
        }
        public string SearchAddress()
        {
            Console.WriteLine("\nInput address:");
            string str = Console.ReadLine();
            return str;
        }
        public int SearchNumberMin()
        {
            Console.WriteLine("\nMin:");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }
        public int SearchNumberMax()
        {
            Console.WriteLine("\nMax:");
            int id = Convert.ToInt32(Console.ReadLine());
            return id;
        }

        //// // // // // // // // // // // /// // // // /// ///
        // // // // // // // // // // // /// // // // /// /// 
        public void AllPatients(List<TPatient> patients)
        {
            Console.WriteLine(" ----Patient:-----");

            Console.WriteLine("|{0,3}|{1,30}|{2,10}|{3,3}|{4,6}|{5,7}|{6,6}|", "Id", "Name", "Gender", "Age", "CardId", "Hosp.Id", "Time");
            Console.WriteLine(" -----------------------------------------------------------------------");
            foreach (TPatient p in patients)
            {
                Console.WriteLine("|{0,3}|{1,30}|{2,10}|{3,3}|{4,6}|{5,7}|{6,6}|", p.Id, p.Name, p.Gender, p.Age, p.Card_Id, p.Hospital_Id, p.Time);
            }
        }
        public TPatient InputPatient()
        {
            TPatient p = new TPatient();
            Console.WriteLine(" ----Patient:-----");
            Console.WriteLine("Input data about patient:");
            Console.Write("Name        : ");
            p.Name = Console.ReadLine();
            Console.Write("Gender      : ");
            p.Gender = Console.ReadLine();
            Console.Write("Age         : ");
            p.Age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Card Id     : ");
            p.Card_Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Hospital Id : ");
            p.Hospital_Id = Convert.ToInt32(Console.ReadLine());
            Console.Write("Time        : ");
            p.Time = Convert.ToInt32(Console.ReadLine());

            return p;
        }
        public void SearchPatient()
        {
            Console.WriteLine("\nChoose column of search:");
            Console.WriteLine("  - 1. Name                     ");
            Console.WriteLine("  - 2. Gender                   ");
            Console.WriteLine("  - 3. Age                      ");
            Console.WriteLine("  - 4. Card Id                  ");
            Console.WriteLine("  - 5. Hospital Id              ");
            Console.WriteLine("  - 6. Time of being in hospital");
        }
        public string SearchGender()
        {
            Console.WriteLine("\nInput gender:");
            string str = Console.ReadLine();
            return str;
        }
        public int SearchPatientCardId()
        {
            Console.WriteLine("\nInput card id");
            int cid = Convert.ToInt32(Console.ReadLine());
            return cid;
        }
        public int SearchPatientHospitalId()
        {
            Console.WriteLine("\nInput hospital id");
            int hid = Convert.ToInt32(Console.ReadLine());
            return hid;
        }
        public int SearchPatientTime()
        {
            Console.WriteLine("\nInput patient time of being in hospital:");
            int time = Convert.ToInt32(Console.ReadLine());
            return time;
        }



        ///
        ///
        ///
        public void AllPatientCards(List<TPatientCard> cards)
        {
            Console.WriteLine(" ----Hospital:-----");

            Console.WriteLine("|{0,3}|{1,30}|{2,5}|", "Id", "Address", "Count");
            Console.WriteLine(" -----------------------------------------");
            foreach (TPatientCard h in cards)
            {
                Console.WriteLine("|{0,3}|{1,30}|{2,5}|", h.Id, h.Diagnosis, h.Indication);
            }
        }
        public TPatientCard InputPatientCard()
        {
            TPatientCard pc = new TPatientCard();
            Console.WriteLine(" ----Input:-----");
            Console.WriteLine("Input data about patient card:");
            Console.Write("Diagnosis      : ");
            pc.Diagnosis = Console.ReadLine();
            Console.Write("Indication     : ");
            pc.Indication = Convert.ToInt32(Console.ReadLine());

            return pc;
        }
        public void SearchPacientCard()
        {
            Console.WriteLine("\nChoose column of search:");
            Console.WriteLine("  - 1. Diagnosis    ");
            Console.WriteLine("  - 2. Indication   ");
        }
        public string SearchDiagnosis()
        {
            Console.WriteLine("\nInput diagnosis:");
            string str = Console.ReadLine();
            return str;
        }


        ////////////////////////////////////////////////////////


        public void AllPatientDoctors(List<TPatientDoctor> pd) 
        {
            Console.WriteLine(" -----Patient_Doctor:-----");
            Console.WriteLine("|{0,3}|{1,25}|{2,25}|", "Id", "Patient", "Doctor");
            Console.WriteLine(" -------------------------------------------------------");
            foreach (TPatientDoctor p in pd)
            {
                Console.WriteLine("|{0,3}|{1,25}|{2,25}|", p.Id, p.Patient, p.Doctor);
            }
        }
        public TPatientDoctor InputPatientDoctor()
        {
            TPatientDoctor pd = new TPatientDoctor();
            Console.WriteLine(" ----Input:-----");
            Console.WriteLine("Input data about patient and doctor:");
            Console.Write("Patient name      : ");
            pd.Patient = Console.ReadLine();
            Console.Write("Doctor name       : ");
            pd.Doctor = Console.ReadLine();

            return pd;
        }

        public void SearchPacientDoctor()
        {
            Console.WriteLine("\nChoose column of search:");
            Console.WriteLine("  - 1. Patient    ");
            Console.WriteLine("  - 2. Doctor   ");
        }
        public string SearchPDPatient()
        {
            Console.WriteLine("\nInput patient name:");
            string str = Console.ReadLine();
            return str;
        }
        public string SearchPDDoctor()
        {
            Console.WriteLine("\nInput doctor name:");
            string str = Console.ReadLine();
            return str;
        }
    }
}
