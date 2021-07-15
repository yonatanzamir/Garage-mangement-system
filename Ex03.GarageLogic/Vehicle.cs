using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        private string m_ModelName=String.Empty;
        private string m_LicenseNumber= String.Empty;
        private float m_RemainedEnergy;
        private readonly List<Wheel> r_WheelsCollections;
        private readonly Dictionary<string, Type> r_Dictionary;

        internal Vehicle()
        {
            r_Dictionary = new Dictionary<string, Type>();
            r_WheelsCollections = new List<Wheel>();
            BuildVehicleInfo();
        }

        protected void BuildVehicleInfo()
        {
            r_Dictionary.Add("ModelName", m_ModelName.GetType());
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
            }
        }

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
            }
            set
            {
                m_LicenseNumber = value;
            }
        }
        
        public float Energy
        {
            get
            {
                return m_RemainedEnergy;
            }
            set
            {
                m_RemainedEnergy = value;
            }
        }
        public Dictionary<string, Type> Dictionary
        {
            get
            {
                return r_Dictionary;
            }
        }

        public List<Wheel> WheelsCollections
        {
            get
            {
                return r_WheelsCollections;
            }
        }

        public void InflateAllWheelsToMax()
        {
            foreach (Wheel wheel in r_WheelsCollections)
            {
                wheel.InflateToMaxAir();
            }
        }

        public void InitializeWheels(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                r_WheelsCollections.Add(new Wheel(i_MaxAirPressure));
            }
        }
        public void UpdateVehicleWheels(string i_ManufacturerName, float i_CurrentAirPressure)
        {
            foreach(Wheel wheel in r_WheelsCollections)
            {
                wheel.CurrentAirPressure = i_CurrentAirPressure;
                wheel.ManufacturerName = i_ManufacturerName;
            }
        }

        public override string ToString()
        {
            string vehicleDetails=string.Format("Model Name: {0}{3}License Number: {1}{3}Remained Energy in precents: {2}%{3}", m_ModelName, m_LicenseNumber, m_RemainedEnergy, Environment.NewLine);
            return vehicleDetails;
        }
    }
}
