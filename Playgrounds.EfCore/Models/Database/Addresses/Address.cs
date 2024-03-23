using System.ComponentModel.DataAnnotations.Schema;

namespace Playgrounds.EfCore.Models.Database.Addresses;

[ComplexType]
public sealed record Address(
    string? Country, 
    string? District, 
    string? City, 
    string? Street, 
    int? Building, 
    int? Flat);