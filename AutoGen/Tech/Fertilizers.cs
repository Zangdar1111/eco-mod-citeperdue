namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Services;
    using Eco.Shared.Utils;
    using Gameplay.Systems.Tooltip;
	
    [Serialized]
    [RequiresSkill(typeof(FarmerSkill), 0)]    
    public partial class FertilizersSkill : Skill
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Fertilizers"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Adding additional nutrients to the soil can improve farming yield or, if overdone, ruin it. Level by crafting related recipes."); } }

        public override void OnLevelUp(User user)
        {
            user.Skillset.AddExperience(typeof(SelfImprovementSkill), 20, Localizer.DoStr("for leveling up another specialization."));
        }


        public static ModificationStrategy MultiplicativeStrategy = 
            new MultiplicativeStrategy(new float[] { 1,
                
                1 - 0.5f,
                
                1 - 0.55f,
                
                1 - 0.6f,
                
                1 - 0.65f,
                
                1 - 0.7f,
                
                1 - 0.75f,
                
                1 - 0.8f,
                
            });
        public override ModificationStrategy MultiStrategy { get { return MultiplicativeStrategy; } }
        public static ModificationStrategy AdditiveStrategy =
            new AdditiveStrategy(new float[] { 0,
                
                0.5f,
                
                0.55f,
                
                0.6f,
                
                0.65f,
                
                0.7f,
                
                0.75f,
                
                0.8f,
                
            });
        public override ModificationStrategy AddStrategy { get { return AdditiveStrategy; } }
        public static int[] SkillPointCost = {
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
            1,
            
        };
        public override int RequiredPoint { get { return this.Level < SkillPointCost.Length ? SkillPointCost[this.Level] : 0; } }
        public override int PrevRequiredPoint { get { return this.Level - 1 >= 0 && this.Level - 1 < SkillPointCost.Length ? SkillPointCost[this.Level - 1] : 0; } }
        public override int MaxLevel { get { return 7; } }
        public override int Tier { get { return 2; } }
    }

    [Serialized]
    public partial class FertilizersSkillBook : SkillBook<FertilizersSkill, FertilizersSkillScroll>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fertilizers Skill Book"); } }
    }

    [Serialized]
    public partial class FertilizersSkillScroll : SkillScroll<FertilizersSkill, FertilizersSkillBook>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Fertilizers Skill Scroll"); } }
    }

    [RequiresSkill(typeof(LibrarianSkill), 0)] 
    public partial class FertilizersSkillBookRecipe : Recipe
    {
        public FertilizersSkillBookRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FertilizersSkillBook>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DirtItem>(10),
                new CraftingElement<PlantFibersItem>(30) 
            };
            this.CraftMinutes = new ConstantValue(5);

            this.Initialize(Localizer.DoStr("Fertilizers Skill Book"), typeof(FertilizersSkillBookRecipe));
            CraftingComponent.AddRecipe(typeof(ResearchTableObject), this);
        }
    }
}
