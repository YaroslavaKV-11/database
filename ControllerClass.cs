using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGR_Shevchuk
{
    internal class ControllerClass
    {
        ModelClass modelClass = new ModelClass();
        ViewClass viewClass = new ViewClass();
        public void Run()
        {

            while (true)
            {
                viewClass.Clear();
                viewClass.MainMenu();
                ConsoleKeyInfo key1 = Console.ReadKey();
                if (key1.Key == ConsoleKey.Escape)
                    break;
                else if (key1.Key == ConsoleKey.D1) 
                {
                    viewClass.TMenu();
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1) 
                    {
                        viewClass.AllDoctors(modelClass.AllDoctors());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        int result = modelClass.InputDoctor(viewClass.InputDoctor());
                        if (result == 0)
                        {
                            viewClass.Output("There is no such hospital.");
                        }
                        else if (result == 1) 
                        {
                            viewClass.Output("New raw was inserted.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        viewClass.AllDoctors(modelClass.AllDoctors());
                        int result = modelClass.DeleteDoctor(viewClass.ChooseId());
                        if (result == 0)
                        {
                            viewClass.Output("Error. Probably there is no such doctor, or maybe this row is connected with others");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was removed.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        viewClass.Output("... ");
                        viewClass.AllDoctors(modelClass.AllDoctors());
                        
                        int i = viewClass.ChooseId();
                        int result = modelClass.UpdateDoctor(viewClass.InputDoctor(), i);
                        if (result == 0)
                        {
                            viewClass.Output("There is no such hospital.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was updated.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        viewClass.SearchDoctor();
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1) 
                        {
                            viewClass.AllDoctors(modelClass.SearchDoctorName(viewClass.SearchName()));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            viewClass.AllDoctors(modelClass.SearchDoctorProfile(viewClass.SearchProfile()));
                        }
                        else if (key3.Key == ConsoleKey.D3)
                        {
                            viewClass.AllDoctors(modelClass.SearchDoctorHospital(viewClass.SearchDHospital()));
                        } 
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        int n = viewClass.Generate();
                        modelClass.GenerateDoctors(n);
                        viewClass.Output(" Generated raws: " + n.ToString());
                    }
                    viewClass.Pause();
                }
                else if (key1.Key == ConsoleKey.D2)
                {
                    viewClass.TMenu();
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        viewClass.AllHospitals(modelClass.AllHospitals());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        int result = modelClass.InputHospital(viewClass.InputHospital());
                        if (result == 0)
                        {
                            viewClass.Output("The hospital on this address already exists.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("New raw was inserted.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        viewClass.AllHospitals(modelClass.AllHospitals());
                        int result = modelClass.DeleteHospital(viewClass.ChooseId());
                        if (result == 0)
                        {
                            viewClass.Output("Error. There is no such hospital, or maybe this row is connecter wtith another one");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was removed.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        viewClass.Output("... ");
                        viewClass.AllHospitals(modelClass.AllHospitals());

                        int i = viewClass.ChooseId();
                        int result = modelClass.UpdateHospital(viewClass.InputHospital(), i);
                        if (result == 0)
                        {
                            viewClass.Output("There is no such hospital that you want to update.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was updated.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        viewClass.SearchHospital();
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            viewClass.AllHospitals(modelClass.SearchAddress(viewClass.SearchAddress()));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            viewClass.AllHospitals(modelClass.SearchNumber(viewClass.SearchNumberMin(), viewClass.SearchNumberMax()));
                        }
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        int n = viewClass.Generate();
                        modelClass.GenerateHospitals(n);
                        viewClass.Output(" Generated raws: " + n.ToString());
                    }
                    viewClass.Pause();
                }
                else if (key1.Key == ConsoleKey.D3)
                {
                    viewClass.TMenu();
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        viewClass.AllPatients(modelClass.AllPatients());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        int result = modelClass.InputPatient(viewClass.InputPatient());
                        if (result == -1)
                        {
                            viewClass.Output("There is no such card Id.");
                        }
                        else if (result == 0)
                        {
                            viewClass.Output("There is no such hospital Id.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("New raw was inserted.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        viewClass.AllPatients(modelClass.AllPatients());
                        int result = modelClass.DeletePatient(viewClass.ChooseId());
                        if (result == 0)
                        {
                            viewClass.Output("Error. Invalid data, or you can not remove this patient");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was removed.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        viewClass.Output("... ");
                        viewClass.AllPatients(modelClass.AllPatients());

                        int i = viewClass.ChooseId();

                        int result = modelClass.UpdatePatient(i, viewClass.InputPatient());
                        if (result == 0)
                        {
                            viewClass.Output("There is no such hospital.");
                        }
                        else if (result == -1)
                        {
                            viewClass.Output("There is no such card");
                        }
                        else if (result == -2)
                        {
                            viewClass.Output("There is no such patient that you want to update.");
                        }
                        else if (result == -3)
                        {
                            viewClass.Output("Error. This raw is connected with others.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was updated.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        viewClass.SearchPatient();
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            viewClass.AllPatients(modelClass.SearchPatientName(viewClass.SearchName()));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            viewClass.AllPatients(modelClass.SearchPatientGender(viewClass.SearchGender()));
                        }
                        else if (key3.Key == ConsoleKey.D3)
                        {
                            viewClass.Output("Input range of number of patients:");
                            viewClass.AllPatients(modelClass.SearchAge(viewClass.SearchNumberMin(), viewClass.SearchNumberMax()));
                        }
                        else if (key3.Key == ConsoleKey.D4)
                        {
                            viewClass.AllPatients(modelClass.SearchPatientCard(viewClass.SearchPatientCardId()));
                        }
                        else if (key3.Key == ConsoleKey.D5)
                        {
                            viewClass.AllPatients(modelClass.SearchPatientHospital(viewClass.SearchPatientHospitalId()));
                        }
                        else if (key3.Key == ConsoleKey.D6)
                        {
                            viewClass.Output("Input range of time:");
                            viewClass.AllPatients(modelClass.SearchPatientTime(viewClass.SearchNumberMin(), viewClass.SearchNumberMax()));
                        }
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        int n = viewClass.Generate();
                        modelClass.GeneratePatients(n);
                        viewClass.Output(" Generated raws: " + n.ToString());
                    }
                    viewClass.Pause();
                }
                else if (key1.Key == ConsoleKey.D4)
                {
                    viewClass.TMenu();
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        viewClass.AllPatientCards(modelClass.AllPatientCards());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        int result = modelClass.InputPatientCard(viewClass.InputPatientCard());
                        if (result == 0)
                        {
                            viewClass.Output("This diagnosis already exists.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("New raw was inserted.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        viewClass.AllPatientCards(modelClass.AllPatientCards());
                        int result = modelClass.DeletePatientCard(viewClass.ChooseId());
                        if (result == 0)
                        {
                            viewClass.Output("Error. There is no such card, or maybe this row is connecter wtith another one");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was removed.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        viewClass.Output("... ");
                        viewClass.AllPatientCards(modelClass.AllPatientCards());

                        int i = viewClass.ChooseId();
                        int result = modelClass.UpdatePatientCard(viewClass.InputPatientCard(), i);
                        if (result == 0)
                        {
                            viewClass.Output("There is no such patient card that you want to update.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was updated.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)///////////////////////////////////////////////////
                    {
                        viewClass.SearchPacientCard();
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            viewClass.AllPatientCards(modelClass.SearchDiagnosis(viewClass.SearchDiagnosis()));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            viewClass.AllPatientCards(modelClass.SearchIndication(viewClass.SearchNumberMin(), viewClass.SearchNumberMax()));
                        }
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        int n = viewClass.Generate();
                        modelClass.GeneratePatientCard(n);
                        viewClass.Output(" Generated raws: " + n.ToString());
                    }
                    viewClass.Pause();
                }
                else if (key1.Key == ConsoleKey.D5)
                {
                    viewClass.TMenu();
                    ConsoleKeyInfo key2 = Console.ReadKey();
                    if (key2.Key == ConsoleKey.D1)
                    {
                        viewClass.AllPatientDoctors(modelClass.AllPatientDoctors());
                    }
                    else if (key2.Key == ConsoleKey.D2)
                    {
                        viewClass.AllPatients(modelClass.AllPatients());
                        viewClass.Output("\n");
                        viewClass.AllDoctors(modelClass.AllDoctors());
                        viewClass.Output("\n");
                        int result = modelClass.InputPatientDoctor(viewClass.InputPatientDoctor());
                        if (result == -1)
                        {
                            viewClass.Output("There is no such patient.");
                        }
                        else if (result == 0)
                        {
                            viewClass.Output("There is no such doctor.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("New raw was inserted.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D3)
                    {
                        viewClass.AllPatientDoctors(modelClass.AllPatientDoctors());
                        int result = modelClass.DeletePatientDoctor(viewClass.ChooseId());
                        if (result == 0)
                        {
                            viewClass.Output("Error. There is no such connection between patient and doctor.");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was removed.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D4)
                    {
                        viewClass.AllPatientDoctors(modelClass.AllPatientDoctors());
                        int result = modelClass.UpdatePatientDoctor(viewClass.ChooseId(), viewClass.InputPatientDoctor());
                        if (result == -2)
                        {
                            viewClass.Output("Error. There is no such patient");
                        }
                        else if (result == -1)
                        {
                            viewClass.Output("Error. There is no such doctor");
                        }
                        else if (result == 0)
                        {
                            viewClass.Output("Error. You have chosen non-existent Id");
                        }
                        else if (result == 1)
                        {
                            viewClass.Output("Raw was updated.");
                        }
                    }
                    if (key2.Key == ConsoleKey.D5)
                    {
                        viewClass.SearchPacientDoctor();
                        ConsoleKeyInfo key3 = Console.ReadKey();
                        if (key3.Key == ConsoleKey.D1)
                        {
                            viewClass.AllPatientDoctors(modelClass.SearchPDPatient(viewClass.SearchPDPatient()));
                        }
                        else if (key3.Key == ConsoleKey.D2)
                        {
                            viewClass.AllPatientDoctors(modelClass.SearchPDDoctor(viewClass.SearchPDDoctor()));
                        }
                    }
                    if (key2.Key == ConsoleKey.D6)
                    {
                        int n = viewClass.Generate();
                        modelClass.GeneratePatientDoctor(n);
                        viewClass.Output(" Generated raws: " + n.ToString());
                    }
                    viewClass.Pause();
                }

            }
        }
    }
}
