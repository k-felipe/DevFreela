using System.Reflection.Metadata.Ecma335;

namespace DevFreela.API.Services
{
    public interface IConfigService
    {
        int GetValue();

    }
    public class ConfigService : IConfigService
    {
        private int _value;
        public int GetValue()
        {
            _value++;
            return _value;
        }
    }
}
