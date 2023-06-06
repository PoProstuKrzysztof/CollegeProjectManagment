﻿using CollegeProjectManagment.Core.Enums;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CollegeProjectManagment.Core.Domain.Entities;

[Table("project")]
public class Project
{
    [Key]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.None)]
    [Column("ProjectId")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Title for project is required")]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;
    public string Requirements { get; set; } = string.Empty;
    public int NumberOfMembers { get; set; }
    public string TechnologyStack { get; set; } = string.Empty;

    [JsonConverter(typeof(StringEnumConverter))]
    public ICollection<ProgrammingLanguages>? ProgrammingLanguages { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public DifficultyLevel DifficultyLevel { get; set; }

    [JsonConverter(typeof(StringEnumConverter))]
    public ProjectState State { get; set; }

    public DateTime PlannedEndDate { get; set; }
    public DateTime? CompletionDate { get; set; }
    public string RepositoryLink { get; set; } = string.Empty;

    //Relationships

    public int? AssignedTeamId { get; set; }
    public Team? Team { get; set; }
    public int? LeaderId { get; set; }

    public Member? Leader { get; set; }
    //[AttributeUsage(AttributeTargets.Property)]
    //public class RepositoryLinkValidationAttribute : ValidationAttribute
    //{
    //    private readonly ProjectState _state;

    //    public RepositoryLinkValidationAttribute(ProjectState state)
    //    {
    //        _state = state;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var project = (Project)validationContext.ObjectInstance;

    //        if (project.State != _state && !string.IsNullOrWhiteSpace(project.RepositoryLink))
    //        {
    //            return new ValidationResult("Cannot add or update repository link until project is finished");
    //        }

    //        return ValidationResult.Success;
    //    }
    //}
}