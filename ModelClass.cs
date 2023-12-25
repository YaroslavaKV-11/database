using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Npgsql;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Runtime.Intrinsics.Arm;

namespace RGR_Shevchuk
{
    #region DbTablesClasses
    public class TDoctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Profile { get; set; }
        public int Hospital { get; set; }
    }
    public class THospital
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int Number { get; set; }
    }
    public class TPatient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int Card_Id { get; set; }
        public int Hospital_Id { get; set; }
        public int Time { get; set; }
    }
    public class TPatientCard
    {
        public int Id { get; set; }
        public string Diagnosis { get; set; }
        public double Indication { get; set; }
    }
    public class TPatientDoctor
    {
        public int Id { get; set; }
        public string Patient { get; set; }
        public string Doctor { get; set; }
    }
    #endregion
    public class HospitalContext : DbContext
    {
        public DbSet<TDoctor> Doctor { get; set; }
        public DbSet<THospital> Hospital { get; set; }
        public DbSet<TPatient> Patient { get; set; }
        public DbSet<TPatientCard> Patient_Card { get; set; }
        public DbSet<TPatientDoctor> Patient_Doctor { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(
                "Host=localhost;Port=5432;Database=Systems for tracking patients' health;Username=postgres;Password=12345");
        }
    }
    class ModelClass
    {
        HospitalContext context { get; set; }
        public ModelClass()
        {
            context = new HospitalContext();
        }

        
        public List<TDoctor> AllDoctors()
        {
            var docs = context.Doctor.FromSqlRaw(
          @"SELECT ""Doctor"".""Id"", 
          ""Doctor"".""Name"" AS Name, 
          ""Doctor"".""Profile"" AS Profile, 
          ""Doctor"".""Hospital_Id"" AS Hospital 
          FROM ""Doctor""");

            return docs.ToList();
        }
        public int InputDoctor(TDoctor doc)
        {
            int id = -1;

            var hospitals = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");

            foreach (THospital h in hospitals)
            {
                if (h.Id == doc.Hospital)
                    id = h.Id;
            }
            if (id == -1)
                return 0;
            else
            {
                var docs = context.Doctor.FromSqlRaw(
                  @"SELECT ""Doctor"".""Id"", 
                  ""Doctor"".""Name"" AS Name, 
                  ""Doctor"".""Profile"" AS Profile, 
                  ""Doctor"".""Hospital_Id"" AS Hospital 
                  FROM ""Doctor""");
                List<int> ids = new List<int>();
                foreach (TDoctor d in docs)
                {
                    ids.Add(d.Id);
                }
                doc.Id = ids.Max(id => id) + 1;
                var sqlQuery = $"INSERT INTO \"Doctor\" VALUES ('{doc.Id}', '{doc.Name}', '{doc.Profile}','{doc.Hospital}')";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
                return 1;
            }
        }
        public int DeleteDoctor(int id) 
        {
            try
            {
                var docs = context.Doctor.FromSqlRaw(
                  @"SELECT ""Doctor"".""Id"", 
                  ""Doctor"".""Name"" AS Name, 
                  ""Doctor"".""Profile"" AS Profile, 
                  ""Doctor"".""Hospital_Id"" AS Hospital 
                  FROM ""Doctor""");
                List<int> ids = new List<int>();
                foreach (TDoctor d in docs)
                {
                    ids.Add(d.Id);
                }
                if (!ids.Contains(id))
                    return 0;
                var sqlQuery = $"DELETE from \"Doctor\" WHERE \"Id\" = {id};";
                int delete = context.Database.ExecuteSqlRaw(sqlQuery);
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public int UpdateDoctor(TDoctor doc, int id_)
        {
            try
            {
                var sqlQuery = $"UPDATE \"Doctor\" SET \"Name\"='{doc.Name}', \"Profile\"='{doc.Profile}', \"Hospital_Id\"={doc.Hospital} WHERE \"Id\" = {id_};";
                int updated = context.Database.ExecuteSqlRaw(sqlQuery);
                context.SaveChangesAsync();
                context = new HospitalContext();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public List<TDoctor> SearchDoctorName(string nm)
        {
            var docs = context.Doctor.FromSqlRaw(
          @"SELECT ""Doctor"".""Id"", 
          ""Doctor"".""Name"" AS Name, 
          ""Doctor"".""Profile"" AS Profile, 
          ""Doctor"".""Hospital_Id"" AS Hospital 
          FROM ""Doctor"" 
          WHERE ""Doctor"".""Name"" LIKE '%'||{0}||'%'", nm);

            return docs.ToList();
        }
        public List<TDoctor> SearchDoctorProfile(string pf)
        {
            var docs = context.Doctor.FromSqlRaw(
          @"SELECT ""Doctor"".""Id"", 
          ""Doctor"".""Name"" AS Name, 
          ""Doctor"".""Profile"" AS Profile, 
          ""Doctor"".""Hospital_Id"" AS Hospital 
          FROM ""Doctor"" 
          WHERE ""Doctor"".""Profile"" LIKE '%'||{0}||'%'", pf);

            return docs.ToList();
        }
        public List<TDoctor> SearchDoctorHospital(int hp)
        {
            var docs = context.Doctor.FromSqlRaw(
          @"SELECT ""Doctor"".""Id"", 
          ""Doctor"".""Name"" AS Name, 
          ""Doctor"".""Profile"" AS Profile, 
          ""Doctor"".""Hospital_Id"" AS Hospital 
          FROM ""Doctor"" 
          WHERE ""Doctor"".""Hospital_Id"" = {0}", hp);
            
            return docs.ToList();
        }
        public void GenerateDoctors(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var docs = context.Doctor.FromSqlRaw(
                  @"SELECT ""Doctor"".""Id"", 
                  ""Doctor"".""Name"" AS Name, 
                  ""Doctor"".""Profile"" AS Profile, 
                  ""Doctor"".""Hospital_Id"" AS Hospital 
                  FROM ""Doctor""");
                List<int> ids = new List<int>();
                List<int> hosps = new List<int>();

                foreach (TDoctor d in docs)
                {
                    ids.Add(d.Id);
                }
                int Id = ids.Max(id => id) + 1;

                var hospitals = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
                foreach (THospital h in hospitals)
                {
                    hosps.Add(h.Id);
                }
                Random rnd = new Random();
                int hosp_id = hosps[rnd.Next(0, hosps.Count)];

                var sqlQuery = $"INSERT INTO \"Doctor\" VALUES ('{Id}', " +
                    $"concat(chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int)), " +
                    $"concat(chr(trunc(65+random()*25)::int)," +
                    $"chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int)),'{hosp_id}')";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        }

        public List<THospital> AllHospitals()
        {
            var hsp = context.Hospital.FromSqlRaw(
          @"SELECT * FROM ""Hospital""");

            return hsp.ToList();
        }
        public int InputHospital(THospital hsp)
        {
            var hospitals = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
            List<int> ids = new List<int>();
            foreach (THospital h in hospitals)
            {
                ids.Add(h.Id);
                if (hsp.Address == h.Address)
                    return 0;
            }
            hsp.Id = ids.Max(id => id) + 1;
            var sqlQuery = $"INSERT INTO \"Hospital\" VALUES ('{hsp.Id}', '{hsp.Address}', '{hsp.Number}')";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        }
        public int DeleteHospital(int id)
        {
            var hospitals = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
            List<int> ids = new List<int>();
            foreach (THospital h in hospitals)
            {
                ids.Add(h.Id);
            }
            if (ids.Contains(id))
            {
                try
                {
                    var sqlQuery = $"DELETE from \"Hospital\" WHERE \"Id\" = {id};";
                    int delete = context.Database.ExecuteSqlRaw(sqlQuery);
                    return 1;
                }
                catch
                {
                    return 0;
                }
                
            }
            else return 0;
        }
        public int UpdateHospital(THospital hsp, int id_)
        {
            var hospitals = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
            List<int> ids = new List<int>();
            foreach (THospital h in hospitals)
            {
                ids.Add(h.Id);
            }
            if (ids.Contains(id_))
            {
                var sqlQuery = $"UPDATE \"Hospital\" SET \"Address\"='{hsp.Address}'," +
                    $" \"Number\"='{hsp.Number}' WHERE \"Id\" = {id_};";
                int updated = context.Database.ExecuteSqlRaw(sqlQuery);
                context.SaveChangesAsync();
                context = new HospitalContext();
                return 1;
            }
            else return 0;
        }
        public List<THospital> SearchAddress(string ad)
        {
            var docs = context.Hospital.FromSqlRaw(
          @"SELECT * 
          FROM ""Hospital"" 
          WHERE ""Hospital"".""Address"" LIKE '%'||{0}||'%'", ad);

            return docs.ToList();
        }
        public List<THospital> SearchNumber(int min, int max)
        {
            var docs = context.Hospital.FromSqlRaw(
          @"SELECT * 
          FROM ""Hospital"" 
          WHERE ""Hospital"".""Number"" >= {0} and ""Hospital"".""Number"" <= {1}", min, max);

            return docs.ToList();
        }
        public void GenerateHospitals(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var hospitals = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
                List<int> ids = new List<int>();

                foreach (THospital h in hospitals)
                {
                    ids.Add(h.Id);
                }
                int Id = ids.Max(id => id) + 1;

                var sqlQuery = $"INSERT INTO \"Hospital\" VALUES ('{Id}', " +
                    $"concat(chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int)," +
                    $"chr(trunc(65+random()*25)::int), ' ', chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int), ' ', " +
                    $"trunc(random()*100)::int), trunc(random()*2000)::int)";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        }


        /////////////////////////////////////////////////////////////////////
        
        public List<TPatient> AllPatients()
        {
            var pts = context.Patient.FromSqlRaw(
              @"SELECT * 
              FROM ""Patient""");

            return pts.ToList();
        }
        public int InputPatient(TPatient pat)
        {
            int cid = -1;
            int hid = -1;

            var pats = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
            foreach (TPatientCard tp in pats)
            {
                if (tp.Id == pat.Card_Id)
                    cid = tp.Id;
            }

            var hosps = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
            foreach (THospital h in hosps)
            {
                if (h.Id == pat.Hospital_Id)
                    hid = h.Id;
            }
            if (cid == -1)
                return -1;
            if (hid == -1)
                return 0;
            else
            {
                var pts = context.Patient.FromSqlRaw(
                  @"SELECT *
                  FROM ""Patient""");
                List<int> ids = new List<int>();
                foreach (TPatient p in pts)
                {
                    ids.Add(p.Id);
                }
                pat.Id = ids.Max(id => id) + 1;
                var sqlQuery = $"INSERT INTO \"Patient\" VALUES ('{pat.Id}', '{pat.Name}', '{pat.Gender}','{pat.Age}' ,'{pat.Card_Id}',{pat.Hospital_Id}, {pat.Time})";
                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
                return 1;
            }
        }
        public int DeletePatient(int id)
        {
            var hospitals = context.Database.SqlQuery<TPatient>($"Select * from \"Patient\"");
            List<int> ids = new List<int>();
            foreach (TPatient p in hospitals)
            {
                ids.Add(p.Id);
            }
            if (ids.Contains(id))
            {
                try
                {
                    var sqlQuery = $"DELETE from \"Patient\" WHERE \"Id\" = {id};";
                    int delete = context.Database.ExecuteSqlRaw(sqlQuery);
                    return 1;
                }
                catch
                {
                    return 0;
                }

            }
            else return 0;
        }

        public int UpdatePatient(int id, TPatient pat)
        {
            int cid = -1;
            int hid = -1;

            var pats = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
            foreach (TPatientCard tp in pats)
            {
                if (tp.Id == pat.Card_Id)
                    cid = tp.Id;
            }

            var hosps = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
            foreach (THospital h in hosps)
            {
                if (h.Id == pat.Hospital_Id)
                    hid = h.Id;
            }
            if (cid == -1)
                return -1;
            if (hid == -1)
                return 0;
            else
            {
                var hospitals = context.Database.SqlQuery<TPatient>($"Select * from \"Patient\"");
                List<int> ids = new List<int>();
                foreach (TPatient p in hospitals)
                {
                    ids.Add(p.Id);
                }
                if (ids.Contains(id))
                {
                    try
                    {
                        var sqlQuery = $"UPDATE \"Patient\" SET \"Name\"='{pat.Name}',  \"Gender\"='{pat.Gender}', \"Age\" = {pat.Age}, \"Card_Id\" = {pat.Card_Id}, \"Hospital_Id\" = {pat.Hospital_Id}, \"Time\" = {pat.Time} WHERE \"Id\" = {id};";

                        int updated = context.Database.ExecuteSqlRaw(sqlQuery);
                        context.SaveChangesAsync();
                        context = new HospitalContext();
                        return 1;
                    }
                    catch
                    {
                        return -3;
                    }

                }
                else return -2;
                
            }
        }
        public List<TPatient> SearchPatientName (string name)
        {
            var pat = context.Patient.FromSqlRaw(
          @"SELECT * 
          FROM ""Patient"" 
          WHERE ""Patient"".""Name"" LIKE '%'||{0}||'%'", name);

            return pat.ToList();
        }
        public List<TPatient> SearchPatientGender(string gender)
        {
            var pat = context.Patient.FromSqlRaw(
          @"SELECT * 
          FROM ""Patient"" 
          WHERE ""Patient"".""Gender"" LIKE '%'||{0}||'%'", gender);

            return pat.ToList();
        }
        public List<TPatient> SearchAge(int min, int max) 
        {
            var docs = context.Patient.FromSqlRaw(
          @"SELECT * 
          FROM ""Patient"" 
          WHERE ""Patient"".""Age"" >= {0} and ""Patient"".""Age"" <= {1}", min, max);

            return docs.ToList();
        }

        public List<TPatient> SearchPatientCard(int id)
        {
            var docs = context.Patient.FromSqlRaw(
          @"SELECT * 
          FROM ""Patient"" 
          WHERE ""Patient"".""Card_Id"" = {0}", id);

            return docs.ToList();
        }
        public List<TPatient> SearchPatientHospital(int id)
        {
            var docs = context.Patient.FromSqlRaw(
              @"SELECT * 
              FROM ""Patient"" 
              WHERE ""Patient"".""Hospital_Id"" = {0}", id);

            return docs.ToList();
        }
        public List<TPatient> SearchPatientTime(int min, int max)
        {
            var docs = context.Patient.FromSqlRaw(
              @"SELECT * 
              FROM ""Patient"" 
              WHERE ""Patient"".""Time"" >= {0} and ""Patient"".""Time"" <= {1}", min, max);

            return docs.ToList();
        }
        public void GeneratePatients(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var pats = context.Database.SqlQuery<TPatient>($"Select * from \"Patient\"");
                List<int> ids = new List<int>();

                foreach (TPatient tp in pats)
                {
                    ids.Add(tp.Id);
                }
                int Id = ids.Max(id => id) + 1;

                List<int> cards = new List<int>();
                List<int> hosps = new List<int>();
                
                var cds = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
                foreach (TPatientCard tc in cds) 
                {
                    cards.Add(tc.Id);
                }

                var hsp = context.Database.SqlQuery<THospital>($"Select * from \"Hospital\"");
                foreach (THospital tc in hsp)
                {
                    hosps.Add(tc.Id);
                }

                Random rnd = new Random();
                int card_id = cards[rnd.Next(0, cards.Count)];
                int hosp_id = hosps[rnd.Next(0, hosps.Count)];

                List<string> genders = new List<string> { "Male", "Female", "Other" };
                string gender = genders[rnd.Next(0, genders.Count)];

                var sqlQuery = $"INSERT INTO \"Patient\" VALUES ('{Id}', " +
                    $"concat(chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int)," +
                    $"chr(trunc(65+random()*25)::int), ' ', chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int) ), " +
                    $"'{gender}', trunc(random()*100)::int, {card_id}, {hosp_id}, trunc(random()*100)::int);";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        }

        /// /// /// /// /// /// /// /// //
        /// 
        public List<TPatientCard> AllPatientCards()
        {
            var ptntcd = context.Patient_Card.FromSqlRaw(
          @"SELECT * FROM ""Patient_Card""");

            return ptntcd.ToList();
        }
        public int InputPatientCard(TPatientCard card)
        {
            var cards = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
            List<int> ids = new List<int>();
            foreach (TPatientCard h in cards)
            {
                ids.Add(h.Id);
                if (card.Diagnosis == h.Diagnosis)
                    return 0;
            }
            card.Id = ids.Max(id => id) + 1;
            var sqlQuery = $"INSERT INTO \"Patient_Card\" VALUES ('{card.Id}', '{card.Diagnosis}', '{card.Indication}')";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        }
        public int DeletePatientCard(int id)
        {
            var cards = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
            List<int> ids = new List<int>();
            foreach (TPatientCard c in cards)
            {
                ids.Add(c.Id);
            }
            if (ids.Contains(id))
            {
                try
                {
                    var sqlQuery = $"DELETE from \"Patient_Card\" WHERE \"Id\" = {id};";
                    int delete = context.Database.ExecuteSqlRaw(sqlQuery);
                    return 1;
                }
                catch
                {
                    return 0;
                }

            }
            else return 0;
        }
        public int UpdatePatientCard(TPatientCard hsp, int id_)
        {
            var cards = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
            List<int> ids = new List<int>();
            foreach (TPatientCard c in cards)
            {
                ids.Add(c.Id);
            }
            if (ids.Contains(id_))
            {
                var sqlQuery = $"UPDATE \"Patient_Card\" SET \"Diagnosis\"='{hsp.Diagnosis}'," +
                    $" \"Indication\"='{hsp.Indication}' WHERE \"Id\" = {id_};";
                int updated = context.Database.ExecuteSqlRaw(sqlQuery);
                context.SaveChangesAsync();
                context = new HospitalContext();
                return 1;
            }
            else return 0;
        }
        public List<TPatientCard> SearchDiagnosis(string ad)
        {
            var cards = context.Patient_Card.FromSqlRaw(
          @"SELECT * 
          FROM ""Patient_Card"" 
          WHERE ""Patient_Card"".""Diagnosis"" LIKE '%'||{0}||'%'", ad);

            return cards.ToList();
        }
        public List<TPatientCard> SearchIndication(int min, int max)
        {
            var cards = context.Patient_Card.FromSqlRaw(
          @"SELECT * 
          FROM ""Patient_Card"" 
          WHERE ""Patient_Card"".""Indication"" >= {0} and ""Patient_Card"".""Indication"" <= {1}", min, max);

            return cards.ToList();
        }
        public void GeneratePatientCard(int n)
        {
            for (int i = 0; i < n; i++)
            {
                var hospitals = context.Database.SqlQuery<TPatientCard>($"Select * from \"Patient_Card\"");
                List<int> ids = new List<int>();

                foreach (TPatientCard h in hospitals)
                {
                    ids.Add(h.Id);
                }
                int Id = ids.Max(id => id) + 1;

                var sqlQuery = $"INSERT INTO \"Patient_Card\" VALUES ('{Id}', " +
                    $"concat(chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int)," +
                    $"chr(trunc(65+random()*25)::int), ' ', chr(trunc(65+random()*25)::int), " +
                    $"chr(trunc(65+random()*25)::int), chr(trunc(65+random()*25)::int), ' ', " +
                    $"trunc(random()*100)::int), trunc(random()*100)::int)";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        }

        //////////////
        //////////////
        ///

        public List<TPatientDoctor> AllPatientDoctors()
        {
            var pd = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""");

            return pd.ToList();
        }
        public int InputPatientDoctor(TPatientDoctor pd)
        {
            int patid = -1;
            int docid = -1;
            List<int> ids = new List<int>();

            var pt = context.Database.SqlQuery<TPatient>($"Select * from \"Patient\"");
            foreach (TPatient p in pt)
            {
                if (p.Name == pd.Patient)
                    patid = p.Id;
            }

            var dc = context.Doctor.FromSqlRaw(
              @"SELECT ""Doctor"".""Id"", 
              ""Doctor"".""Name"" AS Name, 
              ""Doctor"".""Profile"" AS Profile, 
              ""Doctor"".""Hospital_Id"" AS Hospital 
              FROM ""Doctor""");

            foreach (TDoctor d in dc) 
            {
                if (d.Name == pd.Doctor)
                    docid = d.Id;
            }

            if (patid == -1)
                return -1;
            if (docid == -1)
                return 0;

            var pds = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""");

            foreach (TPatientDoctor tpd in pds) 
            {
                ids.Add(tpd.Id);
            }

            pd.Id = ids.Max(id => id) + 1;

            var sqlQuery = $"INSERT INTO \"Patient_Doctor\" VALUES ('{pd.Id}', '{patid}', '{docid}')";
            int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            return 1;
        }

        public int DeletePatientDoctor(int id)
        {
            var pds = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""");
            List<int> ids = new List<int>();
            foreach (TPatientDoctor pd in pds)
            {
                ids.Add(pd.Id);
            }
            if (ids.Contains(id))
            {
                try
                {
                    var sqlQuery = $"DELETE from \"Patient_Doctor\" WHERE \"Id\" = {id};";
                    int delete = context.Database.ExecuteSqlRaw(sqlQuery);
                    return 1;
                }
                catch
                {
                    return 0;
                }

            }
            else return 0;
        }

        public int UpdatePatientDoctor(int id, TPatientDoctor pd)
        {
            var pds = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""");

            List<int> ids = new List<int>();
            foreach (TPatientDoctor pd_ in pds)
            {
                ids.Add(pd_.Id);
            }

            if (ids.Contains(id))
            {
                int patid = -1;
                int docid = -1;

                var pt = context.Database.SqlQuery<TPatient>($"Select * from \"Patient\"");
                foreach (TPatient p in pt)
                {
                    if (p.Name == pd.Patient)
                        patid = p.Id;
                }

                var dc = context.Doctor.FromSqlRaw(
                  @"SELECT ""Doctor"".""Id"", 
                  ""Doctor"".""Name"" AS Name, 
                  ""Doctor"".""Profile"" AS Profile, 
                  ""Doctor"".""Hospital_Id"" AS Hospital 
                  FROM ""Doctor""");

                foreach (TDoctor d in dc)
                {
                    if (d.Name == pd.Doctor)
                        docid = d.Id;
                }

                if (patid == -1)
                    return -2;
                if (docid == -1)
                    return -1;

                var sqlQuery = $"UPDATE \"Patient_Doctor\" SET \"Patient_Id\"='{patid}'," +
                    $" \"Doctor_Id\"='{docid}' WHERE \"Id\" = {id};";
                int updated = context.Database.ExecuteSqlRaw(sqlQuery);
                context.SaveChangesAsync();
                context = new HospitalContext();
                return 1;
            }
            else return 0;
        }

        public List<TPatientDoctor> SearchPDPatient(string name)
        {
            var pd = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""
              AND ""Patient"".""Name"" LIKE '%'||{0}||'%'", name);

            return pd.ToList();
        }
        public List<TPatientDoctor> SearchPDDoctor(string name)
        {
            var pd = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""
              AND ""Doctor"".""Name"" LIKE '%'||{0}||'%'", name);

            return pd.ToList();
        }
        public void GeneratePatientDoctor(int n)
        {
            for (int i = 0; i < n; i++)
            {
                List<int> pats = new List<int>();
                List<int> docs = new List<int>();
                List<int> ids = new List<int>();


                var pt = context.Database.SqlQuery<TPatient>($"Select * from \"Patient\"");
                foreach (TPatient p in pt)
                {
                    pats.Add(p.Id);
                }

                var dc = context.Doctor.FromSqlRaw(
                  @"SELECT ""Doctor"".""Id"", 
              ""Doctor"".""Name"" AS Name, 
              ""Doctor"".""Profile"" AS Profile, 
              ""Doctor"".""Hospital_Id"" AS Hospital 
              FROM ""Doctor""");

                foreach (TDoctor d in dc)
                {
                    docs.Add(d.Id);
                }

                Random rnd = new Random();
                int pid = pats[rnd.Next(0, pats.Count())];
                int did = docs[rnd.Next(0, docs.Count())];

                var pds = context.Patient_Doctor.FromSqlRaw(
              @"SELECT ""Patient_Doctor"".""Id"", 
              ""Patient"".""Name"" AS Patient, 
              ""Doctor"".""Name"" AS Doctor  
              FROM ""Patient"", ""Doctor"", ""Patient_Doctor""
              WHERE ""Patient_Doctor"".""Patient_Id"" = ""Patient"".""Id""  
              AND ""Patient_Doctor"".""Doctor_Id"" = ""Doctor"".""Id""");

                foreach (TPatientDoctor tpd in pds)
                {
                    ids.Add(tpd.Id);
                }

                int id = ids.Max(id => id) + 1;

                var sqlQuery = $"INSERT INTO \"Patient_Doctor\" VALUES ('{id}', '{pid}', '{did}' )";

                int insert = context.Database.ExecuteSqlRaw(sqlQuery);
            }
        }
    }
}
