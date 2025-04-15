using System;
using P4_WebMVC.Models.DomainModels;

namespace P4_WebMVC.Models.ViewModels;

public class NoticeViewModel
{


    public List<Notice> Notices { get; set; } = [];   

    public Notice? Notice { get; set; } = null; 
  


}
