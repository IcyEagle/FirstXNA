using XNA.model.grid;

namespace XNA.model.character
{
    public class Character : ActiveObject
    {
        public string Name;
        public int Level;

        //private Bag bag;
        private CharacterMoves _moves;

        public Character(string name, int level)
        {
            Name = name;
            Level = level;
            //bag = new Bag();
            _moves = new CharacterMoves(this);

            GameModel.Instance.GenericFactory.CreateTestBlock("ground", 800, 700);
        }

        public override void Activate(ActiveObject caller)
        {
            caller.Activate(this);
        }

        public override void Deactivate(ActiveObject caller)
        {
            //caller.Deactivate(this);
        }
        
    }
}
