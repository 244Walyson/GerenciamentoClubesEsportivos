﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GerenciamentoClubesEsportivos.Models.Entities;
using GerenciamentoClubesEsportivos.Utils.Factories;
using GerenciamentoClubesEsportivos.Utils.Interfaces;

namespace GerenciamentoClubesEsportivos.Controllers
{
    internal class MemberController
    {
        private ICRUDRepository<Member> repository;

        public MemberController(ICRUDRepository<Member> repository)
        {
            this.repository = repository;
        }

        public void AddMember(string name, string cpf, string email, string phoneNumber, string cep, DateTime membershipDate)
        {
            Member member = MemberFactory.Build(name, cpf, email, phoneNumber, cep, membershipDate);

            var context = new ValidationContext(member);
            var results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(member, context, results, true);

            if (!isValid)
                throw new Exception(results[0].ErrorMessage);

            repository.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return repository.GetAll();
        }
    }
}
