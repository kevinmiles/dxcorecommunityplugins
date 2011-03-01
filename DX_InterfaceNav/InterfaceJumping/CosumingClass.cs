
namespace InterfaceJumping
{
    public class CosumingClass
    {
        public void JumpToInterface(IAnInterface myObject)
        {
            // so from here I would click on either the parameter or use below and say take me to implemention (select appropriate),
            // alternatively I may say go to Interface

            myObject.DoesSomething();
        }
    }
}
