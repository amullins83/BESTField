using System.Net;

namespace BEST2014
{
    public interface IFieldFactory
    {
        IField Create();
        IField Create(IPAddress address);
        IField Create(string address);
        IField Create(int id);
        IField Create(int id, IPAddress address);
        IField Create(int id, string address);
    }
}
