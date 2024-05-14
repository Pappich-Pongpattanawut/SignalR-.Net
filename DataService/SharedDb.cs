using System.Collections.Concurrent; 
using FormulaOne.ChatService.Models; 
// using FormulaOne.ChatService.DataService;

namespace FormulaOne.ChatService.DataService

{
    public class SharedDb
    {
        //collect userConnection
        private readonly ConcurrentDictionary<string, UserConnection> _connections = new ConcurrentDictionary<string, UserConnection>();

        public ConcurrentDictionary<string, UserConnection> Connections => _connections;
    }
}
