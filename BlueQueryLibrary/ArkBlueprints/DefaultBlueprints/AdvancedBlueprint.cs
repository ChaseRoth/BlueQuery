﻿using System.Collections.Generic;
using System.Linq;
using System;
using BlueQueryLibrary.Lang;

namespace BlueQueryLibrary.ArkBlueprints.DefaultBlueprints
{
    public class AdvancedBlueprint : Blueprint
    {
        public SortedList<string, double> CraftedResources { get; set; }

        // Adding special implmentation to handle the simplebullets
        public override IEnumerable<CalculatedResourceCost> GetResourceCost(Bundle _bundle)
        {
            int amount = (int)_bundle.BundledInformation[BUNDLED_AMOUNT_KEY];
            int calculatedAmount = default;
            // Getting the base resources of this blueprint.
            var resources = base.GetResourceCost(_bundle).ToList();
            // Iterating through the crafted resources that can contain other crafted resources.
            // We want to generate a tree of calculated cost to return.
            for (int i = 0; i < CraftedResources.Count; i++)
            {
                // (resource value * how many) / by how many are produced.
                calculatedAmount = (int)(CraftedResources.Values[i] * amount) / Yield;
                // Appending the new calculated cost

                Bundle extras = new Bundle();
                extras.BundledInformation.Add(BUNDLED_AMOUNT_KEY, calculatedAmount);

                resources.Add(new CalculatedResourceCost
                {
                    Type = CraftedResources.Keys[i],                    
                    Amount = calculatedAmount,
                    // Getting a list of all the resource's that belong to the crafted blueprints nested inside this blueprint and so on.
                    CalculatedResourceCosts = Data.Blueprints.DefaultBlueprints[CraftedResources.Keys[i]].GetResourceCost(extras)
                });
            }                        
            // Returning the structured data representing the entire tree of calculated cost to be crafted.
            return resources;
        }       
    }
}