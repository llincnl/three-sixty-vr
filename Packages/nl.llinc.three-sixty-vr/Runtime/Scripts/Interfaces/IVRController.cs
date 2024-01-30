namespace Interfaces {
    
    /* A viewer controller interface providing abstract controller behaviours */
    public interface IVRController {

        void Initialize();

        void UpdateController() {}
        
        void CleanUp() {}

    }
}