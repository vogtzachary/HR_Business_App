﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Business_App
{
    [Serializable]
    public abstract class Employee
    {
        string firstName;
        string lastName;
        string ssn;
        DateTime hireDate;
        private Benefits benefits;
        public Employee()
        {
            firstName = "N/A";
            lastName = "N/A";
            ssn = "N/A";
            hireDate = new DateTime();
            benefits = new Benefits();
        }
        public Employee(string firstName, string lastName, string ssn, DateTime hireDate, Benefits benefits)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.ssn = ssn;
            this.hireDate = hireDate;
            this.benefits = benefits;
        }

        public override string ToString()
        {
            return "firstName=" + firstName
                 + ", lastName=" + lastName
                 + ", ssn=" + ssn
                 + ", hireDate=" + hireDate.ToShortDateString()
                 + ", healthInsurance=" + benefits.HealthInsurance
                 + ", lifeInsurance=" + benefits.LifeInsurance
                 + ", vacation=" + benefits.Vacation;
        }

        public abstract double CalculatePay();

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string SSN
        {
            get { return ssn; }
            set { ssn = value; }
        }

        public DateTime HireDate
        {
            get { return hireDate; }
            set { hireDate = value; }
        }

        public Benefits BenefitsEmp
        {
            get { return benefits; }
            set { benefits = value; }
        }
    }
}