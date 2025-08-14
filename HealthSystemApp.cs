using System;
using System.Collections.Generic;
using System.Linq;

namespace DCIT318_Assignment.Question2
{
    // Question 2g: Create HealthSystemApp class
    public class HealthSystemApp
    {
        private Repository<Patient> _patientRepo;
        private Repository<Prescription> _prescriptionRepo;
        private Dictionary<int, List<Prescription>> _prescriptionMap;

        public HealthSystemApp()
        {
            _patientRepo = new Repository<Patient>();
            _prescriptionRepo = new Repository<Prescription>();
            _prescriptionMap = new Dictionary<int, List<Prescription>>();
        }

        public void SeedData()
        {
            // Add patients
            _patientRepo.Add(new Patient(1, "John Doe", 35, "Male"));
            _patientRepo.Add(new Patient(2, "Jane Smith", 28, "Female"));
            _patientRepo.Add(new Patient(3, "Bob Johnson", 45, "Male"));

            // Add prescriptions
            _prescriptionRepo.Add(new Prescription(1, 1, "Aspirin", DateTime.Now.AddDays(-5)));
            _prescriptionRepo.Add(new Prescription(2, 1, "Ibuprofen", DateTime.Now.AddDays(-3)));
            _prescriptionRepo.Add(new Prescription(3, 2, "Paracetamol", DateTime.Now.AddDays(-2)));
            _prescriptionRepo.Add(new Prescription(4, 2, "Vitamin D", DateTime.Now.AddDays(-1)));
            _prescriptionRepo.Add(new Prescription(5, 3, "Blood Pressure Medication", DateTime.Now));
        }

        public void BuildPrescriptionMap()
        {
            var allPrescriptions = _prescriptionRepo.GetAll();
            _prescriptionMap = allPrescriptions
                .GroupBy(p => p.PatientId)
                .ToDictionary(g => g.Key, g => g.ToList());
        }

        public List<Prescription> GetPrescriptionsByPatientId(int patientId)
        {
            return _prescriptionMap.ContainsKey(patientId) 
                ? _prescriptionMap[patientId] 
                : new List<Prescription>();
        }

        public void PrintAllPatients()
        {
            Console.WriteLine("All Patients:");
            foreach (var patient in _patientRepo.GetAll())
            {
                Console.WriteLine(patient);
            }
        }

        public void PrintPrescriptionsForPatient(int patientId)
        {
            var prescriptions = GetPrescriptionsByPatientId(patientId);
            var patient = _patientRepo.GetById(p => p.Id == patientId);
            
            Console.WriteLine($"\nPrescriptions for {patient?.Name} (ID: {patientId}):");
            foreach (var prescription in prescriptions)
            {
                Console.WriteLine($"  - {prescription}");
            }
        }
    }
}