// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;
using PublisherData;
using PublisherDomain;

var context = new PubContext();

QueryFilter();

void QueryFilter()
{
    var authors = context.Authors.LastOrDefault(a=>a.LastName=="Lerman");
}