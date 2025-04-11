using System;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Models.ViewModels;

public class HybridViewModel
{

    public List<Blog> Blogs { get; set; } = [];   // blogs ek array hai hybrid viewmodel kay andeer
    public Blog? Blog { get; set; }

    public User? User {get;set;}



}
