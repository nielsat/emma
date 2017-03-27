﻿using MailPusher.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MailPusher.Repository.Seeds
{
    public class NACESeed
    {
        public void Seed()
        {
            List<NACE> result = ParseNACEXML();
            using (var context = new MailPusherDBContext())
            {
                foreach (var item in result)
                {
                    var dbNode = context.NACEs.Add(item);
                    context.SaveChanges();
                    var childNodes = result.FindAll(x => x.ParentCode == item.Code);
                    foreach (var childNode in childNodes)
                    {
                        childNode.ParentId = dbNode.ID;
                    }
                }
            }
        }

        public List<NACE> ParseNACEXML()
        {
            List<NACE> result = new List<NACE>();
            var str = XElement.Parse(xml);
            var tmp = str.Element("body").Element("table").Elements("tr");
            bool skipHeader = true;
            foreach (var item in tmp)
            {
                if (skipHeader)
                {
                    skipHeader = false;
                    continue;
                }
                List<string> tdValues = new List<string>();
                var tdNodes = item.Elements("td");
                foreach (var tdItem in tdNodes)
                {
                    tdValues.Add(tdItem.Value.Replace("\n", "").Replace("\t", ""));
                }
                result.Add(new NACE()
                {
                    Order = Convert.ToInt32(tdValues[0]),
                    Level = Convert.ToInt32(tdValues[1]),
                    Code = tdValues[2],
                    ParentCode = tdValues[3],
                    Description = tdValues[4],
                    ISICRef = tdValues[5],
                    Includes = tdValues[6],
                    IncludesAlso = tdValues[7],
                    Rulings = tdValues[8],
                    Excludes = tdValues[9],
                });
            }
            return result;
        }

        public string xml = @"
        
			
			<html>
			<head>
			<title>Statistical Classification of Economic Activities in the European Community, Rev. 2 (2008)</title>
			</head>
			<body>
			<table width='100%' border='1'>
				<tr>
					<th>Order</th>
					<th>Level</th>
					
						<th>Code</th>
					
						<th>Parent</th>
					
						<th>Description</th>
					
						<th>Reference to ISIC Rev. 4</th>
					
						<th>This item includes</th>
					
						<th>This item also includes</th>
					
						<th>Rulings</th>
					
						<th>This item excludes</th>
					
				</tr>
				
				
	<tr>
		<td valign='top'>
			398481
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				A 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				AGRICULTURE, FORESTRY AND FISHING 
			</td>
		

			<td valign='top'>
				A 
			</td>
		

			<td valign='top'>
				This section includes the exploitation of vegetal and animal natural resources, comprising the activities of growing of crops, raising and breeding of animals, harvesting of timber and other plants, animals or animal products from a farm or their natural habitats. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398482
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				A 
			</td>
		

			<td valign='top'>
				Crop and animal production, hunting and related service activities 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				This division includes two basic activities, namely the production of crop products and production of animal products, covering also the forms of organic agriculture, the growing of genetically modified crops and the raising of genetically modified animals. This division includes growing of crops in open fields as well in greenhouses. Group 01.5 (Mixed farming) breaks with the usual principles for identifying main activity. It accepts that many agricultural holdings have reasonably balanced crop and animal production, and that it would be arbitrary to classify them in one category or the other. 
			</td>
		

			<td valign='top'>
				This division also includes service activities incidental to agriculture, as well as hunting, trapping and related activities. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				Agricultural activities exclude any subsequent processing of the agricultural products (classified under divisions 10 and 11 (Manufacture of food products and beverages) and division 12 (Manufacture of tobacco products)), beyond that needed to prepare them for the primary markets. The preparation of products for the primary markets is included here.The division excludes field construction (e.g. agricultural land terracing, drainage, preparing rice paddies etc.) classified in section F (Construction) and buyers and cooperative associations engaged in the marketing of farm products classified in section G. Also excluded is the landscape care and maintenance, which is classified in class 81.30. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398483
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Growing of non-perennial crops 
			</td>
		

			<td valign='top'>
				011 
			</td>
		

			<td valign='top'>
				This group includes the growing of non-perennial crops, i.e. plants that do not last for more than two growing seasons. Included is the growing of these plants for the purpose of seed production. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398484
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.11 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of cereals (except rice), leguminous crops and oil seeds 
			</td>
		

			<td valign='top'>
				0111 
			</td>
		

			<td valign='top'>
				This class includes all forms of growing of cereals, leguminous crops and oil seeds in open fields. The growing of these crops is often combined within agricultural units.This class includes:- growing of cereals such as:  • wheat  • grain maize  • sorghum  • barley  • rye  • oats  • millets  • other cereals n.e.c.- growing of leguminous crops such as:  • beans  • broad beans  • chick peas  • cow peas  • lentils  • lupines  • peas  • pigeon peas  • other leguminous crops- growing of oil seeds such as:  • soya beans  • groundnuts  • castor bean  • linseed  • mustard seed  • niger seed  • rapeseed  • safflower seed  • sesame seed  • sunflower seed  • other oil seeds 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of rice, see 01.12- growing of sweet corn, see 01.13- growing of maize for fodder, see 01.19- growing of oleaginous fruits, see 01.26 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398485
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.12 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of rice 
			</td>
		

			<td valign='top'>
				0112 
			</td>
		

			<td valign='top'>
				This class includes:- growing of rice (including organic farming and the growing of genetically modified rice) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398486
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.13 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of vegetables and melons, roots and tubers 
			</td>
		

			<td valign='top'>
				0113 
			</td>
		

			<td valign='top'>
				This class includes:- growing of leafy or stem vegetables such as:  • artichokes  • asparagus  • cabbages  • cauliflower and broccoli  • lettuce and chicory  • spinach  • other leafy or stem vegetables- growing of fruit bearing vegetables such as:  • cucumbers and gherkins  • eggplants (aubergines)  • tomatoes  • watermelons  • cantaloupes  • other melons and fruit-bearing vegetables- growing of root, bulb or tuberous vegetables such as:  • carrots  • turnips  • garlic  • onions (incl. shallots)  • leeks and other alliaceous vegetables  • other root, bulb or tuberous vegetables- growing of mushrooms and truffles- growing of vegetable seeds, including sugar beet seeds, excluding other beet seeds- growing of sugar beet- growing of other vegetables- growing of roots and tubers such as:  • potatoes  • sweet potatoes  • cassava  • yams  • other roots and tubers 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of chillies, peppers (capsicum sop.) and other spices and aromatic crops, see 01.28- growing of mushroom spawn, see 01.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398487
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.14 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of sugar cane 
			</td>
		

			<td valign='top'>
				0114 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of sugar beet, see 01.13 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398488
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.15 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of tobacco 
			</td>
		

			<td valign='top'>
				0115 
			</td>
		

			<td valign='top'>
				This class includes:- growing of unmanufactured tobacco 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of tobacco products, see 12.00 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398489
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.16 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of fibre crops 
			</td>
		

			<td valign='top'>
				0116 
			</td>
		

			<td valign='top'>
				This class includes:- growing of cotton- growing of jute, kenaf and other textile bast fibres- growing of flax and true hemp- growing of sisal and other textile fibre of the genus agave- growing of abaca, ramie and other vegetable textile fibres- growing of other fibre crops 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398490
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.19 
			</td>
		

			<td valign='top'>
				01.1 
			</td>
		

			<td valign='top'>
				Growing of other non-perennial crops 
			</td>
		

			<td valign='top'>
				0119 
			</td>
		

			<td valign='top'>
				This class includes the growing of all other non-perennial crops:- growing of swedes, mangolds, fodder roots, clover, alfalfa, sainfoin, fodder maize and other grasses, forage kale and similar forage products - growing of beet seeds (excluding sugar beet seeds) and seeds of forage plants- growing of flowers- production of cut flowers and flower buds- growing of flower seeds 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of non-perennial spices, aromatic, drug and pharmaceutical crops, see 01.28 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398491
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Growing of perennial crops 
			</td>
		

			<td valign='top'>
				012 
			</td>
		

			<td valign='top'>
				This group includes the growing of perennial crops, i.e. plants that lasts for more than two growing seasons, either dying back after each season or growing continuously. Included is the growing of these plants for the purpose of seed production. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398492
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.21 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of grapes 
			</td>
		

			<td valign='top'>
				0121 
			</td>
		

			<td valign='top'>
				This class includes:- growing of wine grapes and table grapes in vineyards 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of wine, see 11.02 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398493
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.22 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of tropical and subtropical fruits 
			</td>
		

			<td valign='top'>
				0122 
			</td>
		

			<td valign='top'>
				This class includes:- growing of tropical and subtropical fruits:  • avocados  • bananas and plantains  • dates  • figs  • mangoes  • papayas  • pineapples  • other tropical and subtropical fruits 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398494
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.23 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of citrus fruits 
			</td>
		

			<td valign='top'>
				0123 
			</td>
		

			<td valign='top'>
				This class includes:- growing of citrus fruits:  • grapefruit and pomelo  • lemons and limes  • oranges  • tangerines, mandarins and clementines  • other citrus fruits 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398495
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.24 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of pome fruits and stone fruits 
			</td>
		

			<td valign='top'>
				0124 
			</td>
		

			<td valign='top'>
				This class includes:- growing of pome fruits and stone fruits:  • apples  • apricots  • cherries and sour cherries  • peaches and nectarines  • pears and quinces  • plums and sloes  • other pome fruits and stone fruits 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398496
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.25 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of other tree and bush fruits and nuts 
			</td>
		

			<td valign='top'>
				0125 
			</td>
		

			<td valign='top'>
				This class includes:- growing of berries:  • blueberries  • currants  • gooseberries  • kiwi fruit  • raspberries  • strawberries  • other berries- growing of fruit seeds- growing of edible nuts:  • almonds  • cashew nuts  • chestnuts  • hazelnuts  • pistachios  • walnuts  • other nuts- growing of other tree and bush fruits:  • locust beans 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of coconuts, see 01.26 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398497
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.26 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of oleaginous fruits 
			</td>
		

			<td valign='top'>
				0126 
			</td>
		

			<td valign='top'>
				This class includes:- growing of oleaginous fruits:  • coconuts  • olives  • oil palms  • other oleaginous fruits 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of soya beans, groundnuts and other oil seeds, see 01.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398498
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.27 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of beverage crops 
			</td>
		

			<td valign='top'>
				0127 
			</td>
		

			<td valign='top'>
				This class includes:- growing of beverage crops:  • coffee  • tea  • maté  • cocoa   • other beverage crops 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398499
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.28 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of spices, aromatic, drug and pharmaceutical crops 
			</td>
		

			<td valign='top'>
				0128 
			</td>
		

			<td valign='top'>
				This class includes:- growing of perennial and non-perennial spices and aromatic crops:  • pepper (piper sop.)  • chillies and peppers (capsicum sop.)  • nutmeg, mace and cardamoms  • anise, badian and fennel  • cinnamon (canella)  • cloves  • ginger  • vanilla  • hops  • other spices and aromatic crops- growing of drug and narcotic crops 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398500
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.29 
			</td>
		

			<td valign='top'>
				01.2 
			</td>
		

			<td valign='top'>
				Growing of other perennial crops 
			</td>
		

			<td valign='top'>
				0129 
			</td>
		

			<td valign='top'>
				This class includes:- growing of rubber trees for harvesting of latex- growing of Christmas trees- growing of trees for extraction of sap- growing of vegetable materials of a kind used primarily for plaiting 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of flowers, production of cut flower buds and growing of flower seeds, see 01.19- gathering of tree sap or rubber-like gums in the wild, see 02.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398501
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.3 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Plant propagation 
			</td>
		

			<td valign='top'>
				013 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398502
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.30 
			</td>
		

			<td valign='top'>
				01.3 
			</td>
		

			<td valign='top'>
				Plant propagation 
			</td>
		

			<td valign='top'>
				0130 
			</td>
		

			<td valign='top'>
				This class includes the production of all vegetative planting materials including cuttings, suckers and seedlings for direct plant propagation or to create plant grafting stock into which selected scion is grafted for eventual planting to produce crops.This class includes:- growing of plants for planting- growing of plants for ornamental purposes, including turf for transplanting- growing of live plants for bulbs, tubers and roots; cuttings and slips; mushroom spawn - operation of tree nurseries, except forest tree nurseries 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Nurturing and selling of tree seedlings 
			</td>
		

			<td valign='top'>
				This class excludes:- growing of plants for the purpose of seed production, see 01.1, 01.2- operation of forest tree nurseries, see 02.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398503
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Animal production 
			</td>
		

			<td valign='top'>
				014 
			</td>
		

			<td valign='top'>
				This group includes raising (farming) and breeding of all animals, except aquatic animals. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes: - farm animal boarding and care, see 01.62- production of hides and skins from slaughterhouses, see 10.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398504
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.41 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of dairy cattle 
			</td>
		

			<td valign='top'>
				0141 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of dairy cattle- production of raw milk from cows or buffaloes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- processing of milk, see 10.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398505
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.42 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of other cattle and buffaloes 
			</td>
		

			<td valign='top'>
				0141 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of cattle and buffaloes for meat- production of bovine semen 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398506
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.43 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of horses and other equines 
			</td>
		

			<td valign='top'>
				0142 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of horses, asses, mules or hinnies 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operation of racing and riding stables, see 93.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398507
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.44 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of camels and camelids 
			</td>
		

			<td valign='top'>
				0143 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of camels (dromedary) and camelids 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398508
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.45 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of sheep and goats 
			</td>
		

			<td valign='top'>
				0144 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of sheep and goats- production of raw sheep or goat milk- production of raw wool 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- sheep shearing on a fee or contract basis, see 01.62- production of pulled wool, see 10.11- processing of milk, see 10.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398509
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.46 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of swine/pigs 
			</td>
		

			<td valign='top'>
				0145 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of swine (pigs) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398510
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.47 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of poultry 
			</td>
		

			<td valign='top'>
				0146 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of poultry:  • chickens, turkeys, ducks, geese and guinea fowls- production of eggs from poultry- operation of poultry hatcheries 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of feathers or down, see 10.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398511
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.49 
			</td>
		

			<td valign='top'>
				01.4 
			</td>
		

			<td valign='top'>
				Raising of other animals 
			</td>
		

			<td valign='top'>
				0149 
			</td>
		

			<td valign='top'>
				This class includes:- raising and breeding of semi-domesticated or other live animals:  • ostriches and emus  • other birds (except poultry)  • insects  • rabbits and other fur animals- production of fur skins, reptile or bird skins from ranching operation- operation of worm farms, land mollusc farms, snail farms etc.- raising of silk worms, production of silk worm cocoons- bee-keeping and production of honey and beeswax- raising and breeding of pet animals (except fish):  • cats and dogs  • birds, such as parakeets etc.  • hamsters etc.- raising of diverse animals 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Production of fresh quail eggs and quail egg for hatching 
			</td>
		

			<td valign='top'>
				This class excludes:- production of hides and skins originating from hunting and trapping, see 01.70- operation of frog farms, crocodile farms, marine worm farms, see 03.21, 03.22- operation of fish farms, see 03.21, 03.22- boarding and training of pet animals, see 96.09- raising and breeding of poultry, see 01.47 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398512
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.5 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Mixed farming 
			</td>
		

			<td valign='top'>
				015 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398513
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.50 
			</td>
		

			<td valign='top'>
				01.5 
			</td>
		

			<td valign='top'>
				Mixed farming 
			</td>
		

			<td valign='top'>
				0150 
			</td>
		

			<td valign='top'>
				This class includes the combined production of crops and animals without a specialised production of crops or animals. The size of the overall farming operation is not a determining factor. If either production of crops or animals in a given unit is 66% or more of standard gross margins, the combined activity should not be included here, but allocated to crop or animal farming. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- mixed crop farming, see groups 01.1 and 01.2- mixed animal farming, see group 01.4 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398514
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.6 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Support activities to agriculture and post-harvest crop activities 
			</td>
		

			<td valign='top'>
				016 
			</td>
		

			<td valign='top'>
				This group includes activities incidental to agricultural production and activities similar to agriculture not undertaken for production purposes (in the sense of harvesting agricultural products), done on a fee or contract basis. 
			</td>
		

			<td valign='top'>
				Also included are post-harvest crop activities, aimed at preparing agricultural products for the primary market. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398515
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.61 
			</td>
		

			<td valign='top'>
				01.6 
			</td>
		

			<td valign='top'>
				Support activities for crop production 
			</td>
		

			<td valign='top'>
				0161 
			</td>
		

			<td valign='top'>
				This class includes:- agricultural activities on a fee or contract basis:  • preparation of fields  • establishing a crop  • treatment of crops  • crop spraying, including by air  • trimming of fruit trees and vines  • transplanting of rice, thinning of beets  • harvesting  • pest control (including rabbits) in connection with agriculture- maintenance of agricultural land in good agricultural and environmental condition- operation of agricultural irrigation equipment 
			</td>
		

			<td valign='top'>
				This class also includes:- provision of agricultural machinery with operators and crew 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- post-harvest crop activities, see 01.63- drainage of agricultural land, see 43.12- landscape architecture, see 71.11- activities of agronomists and agricultural economists, see 74.90- landscape gardening, planting, see 81.30- organisation of agricultural shows and fairs, see 82.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398516
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.62 
			</td>
		

			<td valign='top'>
				01.6 
			</td>
		

			<td valign='top'>
				Support activities for animal production 
			</td>
		

			<td valign='top'>
				0162 
			</td>
		

			<td valign='top'>
				This class includes:- agricultural activities on a fee or contract basis:  • activities to promote propagation, growth and output of animals  • herd testing services, droving services, agistment services, poultry caponising, coop cleaning etc.  • activities related to artificial insemination  • stud services  • sheep shearing  • farm animal boarding and care 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of farriers 
			</td>
		

			<td valign='top'>
				- Automated egg hatching for poultry- Operation of livestock management systems 
			</td>
		

			<td valign='top'>
				This class excludes:- provision of space for animal boarding only, see 68.20- veterinary activities, see 75.00- vaccination of animals, see 75.00- rental of animals (e.g. herds), see 77.39- pet boarding, see 96.09 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398517
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.63 
			</td>
		

			<td valign='top'>
				01.6 
			</td>
		

			<td valign='top'>
				Post-harvest crop activities 
			</td>
		

			<td valign='top'>
				0163 
			</td>
		

			<td valign='top'>
				This class includes:- preparation of crops for primary markets, i.e. cleaning, trimming, grading, disinfecting- cotton ginning- preparation of tobacco leaves, e.g. drying- preparation of cocoa beans, e.g. peeling- waxing of fruit- sun-drying of fruit and vegetables 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Sun-drying of tobacco- Sun-drying of herbs and spices 
			</td>
		

			<td valign='top'>
				This class excludes:- preparation of agricultural products by the producer, see corresponding class in groups 01.1, 01.2 or 01.3- post-harvest activities aimed at improving the propagation quality of seed, see 01.64 - stemming and redrying of tobacco, see 12.00- marketing activities of commission merchants and cooperative associations, see division 46- wholesale of agricultural raw materials, see 46.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398518
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.64 
			</td>
		

			<td valign='top'>
				01.6 
			</td>
		

			<td valign='top'>
				Seed processing for propagation 
			</td>
		

			<td valign='top'>
				0164 
			</td>
		

			<td valign='top'>
				This class includes all post-harvest activities aimed at improving the propagation quality of seed through the removal of non-seed materials, undersized, mechanically or insect-damaged and immature seeds as well as removing the seed moisture to a safe level for seed storage. This activity includes the drying, cleaning, grading and treating of seeds until they are marketed. The treatment of genetically modified seeds is included here. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of seeds, see groups 01.1 and 01.2- processing of seeds to obtain oil, see 10.41- research to develop or modify new forms of seeds, see 72.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398519
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				01.7 
			</td>
		

			<td valign='top'>
				01 
			</td>
		

			<td valign='top'>
				Hunting, trapping and related service activities 
			</td>
		

			<td valign='top'>
				017 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398520
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				01.70 
			</td>
		

			<td valign='top'>
				01.7 
			</td>
		

			<td valign='top'>
				Hunting, trapping and related service activities 
			</td>
		

			<td valign='top'>
				0170 
			</td>
		

			<td valign='top'>
				This class includes:- hunting and trapping on a commercial basis- taking of animals (dead or alive) for food, fur, skin, or for use in research, in zoos or as pets- production of fur skins, reptile or bird skins from hunting or trapping activities 
			</td>
		

			<td valign='top'>
				This class also includes:- land-based catching of sea mammals such as walrus and seal 
			</td>
		

			<td valign='top'>
				- Catching of frogs in the wild 
			</td>
		

			<td valign='top'>
				This class excludes:- production of fur skins, reptile or bird skins from ranching operations, see group 01.49- raising of game animals on ranching operations, see 01.4- catching of whales, see 03.11- production of hides and skins originating from slaughterhouses, see 10.11- hunting for sport or recreation and related service activities, see 93.19- service activities to promote hunting and trapping, see 94.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398521
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				02 
			</td>
		

			<td valign='top'>
				A 
			</td>
		

			<td valign='top'>
				Forestry and logging 
			</td>
		

			<td valign='top'>
				02 
			</td>
		

			<td valign='top'>
				This division includes the production of roundwood as well as the extraction and gathering of wild growing non-wood forest products. Besides the production of timber, forestry activities result in products that undergo little processing, such as firewood, charcoal and roundwood used in an unprocessed form (e.g. pit-props, pulpwood etc.). These activities can be carried out in natural or planted forests. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				Excluded is further processing of wood beginning with sawmilling and planing of wood, see division 16. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398522
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				02.1 
			</td>
		

			<td valign='top'>
				02 
			</td>
		

			<td valign='top'>
				Silviculture and other forestry activities 
			</td>
		

			<td valign='top'>
				021 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398523
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				02.10 
			</td>
		

			<td valign='top'>
				02.1 
			</td>
		

			<td valign='top'>
				Silviculture and other forestry activities 
			</td>
		

			<td valign='top'>
				0210 
			</td>
		

			<td valign='top'>
				This class includes:- growing of standing timber: planting, replanting, transplanting, thinning and conserving of forests and timber tracts- growing of coppice, pulpwood and fire wood- operation of forest tree nurseriesThese activities can be carried out in natural or planted forests. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Growing of paulownia and willow 
			</td>
		

			<td valign='top'>
				This class excludes:- growing of Christmas trees, see 01.29- operation of tree nurseries, except for forest trees, see 01.30- gathering of mushrooms and other wild growing non-wood forest products, see 02.30- production of wood chips and particles, see 16.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398524
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				02.2 
			</td>
		

			<td valign='top'>
				02 
			</td>
		

			<td valign='top'>
				Logging 
			</td>
		

			<td valign='top'>
				022 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398525
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				02.20 
			</td>
		

			<td valign='top'>
				02.2 
			</td>
		

			<td valign='top'>
				Logging 
			</td>
		

			<td valign='top'>
				0220 
			</td>
		

			<td valign='top'>
				This class includes:- production of roundwood for forest-based manufacturing industries- production of roundwood used in an unprocessed form such as pit-props, fence posts and utility poles- gathering and production of wood for energy- gathering and production of forest harvesting residues for energy- production of charcoal in the forest (using traditional methods)The output of this activity can take the form of logs or fire wood. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing of Christmas trees, see 01.29- growing of standing timber: planting, replanting, transplanting, thinning and conserving of forests and timber tracts, see 02.10- gathering of wild growing non-wood forest products, see 02.30- production of wood chips and particles, see 16.10- production of charcoal through distillation of wood, see 20.14 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398526
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				02.3 
			</td>
		

			<td valign='top'>
				02 
			</td>
		

			<td valign='top'>
				Gathering of wild growing non-wood products 
			</td>
		

			<td valign='top'>
				023 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398527
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				02.30 
			</td>
		

			<td valign='top'>
				02.3 
			</td>
		

			<td valign='top'>
				Gathering of wild growing non-wood products 
			</td>
		

			<td valign='top'>
				0230 
			</td>
		

			<td valign='top'>
				This class includes:- gathering of wild growing materials:  • mushrooms, truffles  • berries  • nuts  • balata and other rubber-like gums  • cork  • lac and resins  • balsams  • vegetable hair  • eelgrass  • acorns, horse chestnuts  • mosses and lichens 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- managed production of any of these products (except growing of cork trees), see division 01- growing of mushrooms or truffles, see 01.13- growing of berries or nuts, see 01.25- gathering of fire wood, see 02.20- production of wood chips, see 16.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398528
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				02.4 
			</td>
		

			<td valign='top'>
				02 
			</td>
		

			<td valign='top'>
				Support services to forestry 
			</td>
		

			<td valign='top'>
				024 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398529
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				02.40 
			</td>
		

			<td valign='top'>
				02.4 
			</td>
		

			<td valign='top'>
				Support services to forestry 
			</td>
		

			<td valign='top'>
				0240 
			</td>
		

			<td valign='top'>
				This class includes carrying out part of the forestry operation on a fee or contract basis.This class includes:- forestry service activities:  • forestry inventories  • forest management consulting services  • timber evaluation  • forest fire fighting and protection  • forest pest control- logging service activities:  • transport of logs within the forest 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operation of forest tree nurseries, see 02.10- draining of forestry land, see 43.12- clearing of building sites, see 43.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398530
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				03 
			</td>
		

			<td valign='top'>
				A 
			</td>
		

			<td valign='top'>
				Fishing and aquaculture 
			</td>
		

			<td valign='top'>
				03 
			</td>
		

			<td valign='top'>
				This division includes capture fishery and aquaculture, covering the use of fishery resources from marine, brackish or freshwater environments, with the goal of capturing or gathering fish, crustaceans, molluscs and other marine organisms and products (e.g. aquatic plants, pearls, sponges etc). 
			</td>
		

			<td valign='top'>
				Also included are activities that are normally integrated in the process of production for own account (e.g. seeding oysters for pearl production). Service activities incidental to marine or freshwater fishery or aquaculture are included in the related fishing or aquaculture activities. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division does not include building and repairing of ships and boats (30.1, 33.15) and sport or recreational fishing activities (93.19). Processing of fish, crustaceans or molluscs is excluded, whether at land-based plants or on factory ships (10.20). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398531
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				03.1 
			</td>
		

			<td valign='top'>
				03 
			</td>
		

			<td valign='top'>
				Fishing 
			</td>
		

			<td valign='top'>
				031 
			</td>
		

			<td valign='top'>
				This group includes &quot;capture fishery&quot;, i.e. the hunting, collecting and gathering activities directed at removing or collecting live wild aquatic organisms (predominantly fish, molluscs and crustaceans) including plants from the oceanic, coastal or inland waters for human consumption and other purposes by hand or more usually by various types of fishing gear such as nets, lines and stationary traps. Such activities can be conducted on the intertidal shoreline (e.g. collection of molluscs such as mussels and oysters) or shore based netting, or from home-made dugouts or more commonly using commercially made boats in inshore, coastal waters or offshore waters. Such activities also include fishing in restocked water bodies. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398532
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				03.11 
			</td>
		

			<td valign='top'>
				03.1 
			</td>
		

			<td valign='top'>
				Marine fishing 
			</td>
		

			<td valign='top'>
				0311 
			</td>
		

			<td valign='top'>
				This class includes:- fishing on a commercial basis in ocean and coastal waters- taking of marine crustaceans and molluscs- whale catching- taking of marine aquatic animals: turtles, sea squirts, tunicates, sea urchins etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of vessels engaged both in marine fishing and in processing and preserving of fish- gathering of other marine organisms and materials: natural pearls, sponges, coral and algae 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- capturing of marine mammals, except whales, e.g. walruses, seals, see 01.70- processing of whales on factory ships, see 10.11- processing of fish, crustaceans and molluscs on factory ships or in factories ashore, see 10.20- rental of pleasure boats with crew for sea and coastal water transport (e.g. for fishing cruises), see 50.10- fishing inspection, protection and patrol services, see 84.24- fishing practiced for sport or recreation and related services, see 93.19- operation of sport fishing preserves, see 93.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398533
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				03.12 
			</td>
		

			<td valign='top'>
				03.1 
			</td>
		

			<td valign='top'>
				Freshwater fishing 
			</td>
		

			<td valign='top'>
				0312 
			</td>
		

			<td valign='top'>
				This class includes:- fishing on a commercial basis in inland waters- taking of freshwater crustaceans and molluscs- taking of freshwater aquatic animals 
			</td>
		

			<td valign='top'>
				This class also includes:- gathering of freshwater materials 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- processing of fish, crustaceans and molluscs, see 10.20- fishing inspection, protection and patrol services, see 84.24- fishing practiced for sport or recreation and related services, see 93.19- operation of sport fishing preserves, see 93.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398534
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				03.2 
			</td>
		

			<td valign='top'>
				03 
			</td>
		

			<td valign='top'>
				Aquaculture 
			</td>
		

			<td valign='top'>
				032 
			</td>
		

			<td valign='top'>
				This group includes &quot;aquaculture&quot; (or aquafarming), i.e. the production process involving the culturing or farming (including harvesting) of aquatic organisms (fish, molluscs, crustaceans, plants, crocodiles, alligators and amphibians) using techniques designed to increase the production of the organisms in question beyond the natural capacity of the environment (for example regular stocking, feeding and protection from predators). Culturing/farming refers to the rearing up to their juvenile and/or adult phase under captive conditions of the above organisms. 
			</td>
		

			<td valign='top'>
				In addition, &quot;aquaculture&quot; also encompasses individual, corporate or state ownership of the individual organisms throughout the rearing or culture stage, up to and including harvesting. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398535
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				03.21 
			</td>
		

			<td valign='top'>
				03.2 
			</td>
		

			<td valign='top'>
				Marine aquaculture 
			</td>
		

			<td valign='top'>
				0321 
			</td>
		

			<td valign='top'>
				This class includes:- fish farming in sea water including farming of marine ornamental fish- production of bivalve spat (oyster mussel etc.), lobsterlings, shrimp post-larvae, fish fry and fingerlings- growing of laver and other edible seaweeds- culture of crustaceans, bivalves, other molluscs and other aquatic animals in sea water 
			</td>
		

			<td valign='top'>
				This class also includes:- aquaculture activities in brackish waters- aquaculture activities in salt water filled tanks and reservoirs- operation of fish hatcheries (marine)- operation of marine worm farms 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- frog farming, see 03.22- operation of sport fishing preserves, see 93.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398536
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				03.22 
			</td>
		

			<td valign='top'>
				03.2 
			</td>
		

			<td valign='top'>
				Freshwater aquaculture 
			</td>
		

			<td valign='top'>
				0322 
			</td>
		

			<td valign='top'>
				This class includes:- fish farming in freshwater including farming of freshwater ornamental fish- culture of freshwater crustaceans, bivalves, other molluscs and other aquatic animals- operation of fish hatcheries (freshwater)- farming of frogs 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Farming of frogs in freshwater 
			</td>
		

			<td valign='top'>
				This class excludes:- aquaculture activities in salt water filled tanks and reservoirs, see 03.21- operation of sport fishing preserves, see 93.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398537
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				B 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				MINING AND QUARRYING 
			</td>
		

			<td valign='top'>
				B 
			</td>
		

			<td valign='top'>
				Mining and quarrying include the extraction of minerals occurring naturally as solids (coal and ores), liquids (petroleum) or gases (natural gas). Extraction can be achieved by different methods such as underground or surface mining, well operation, seabed mining etc.This section includes supplementary activities aimed at preparing the crude materials for marketing, for example, crushing, grinding, cleaning, drying, sorting, concentrating ores, liquefaction of natural gas and agglomeration of solid fuels. These operations are often accomplished by the units that extracted the resource and/or others located nearby.Mining activities are classified into divisions, groups and classes on the basis of the principal mineral produced. Divisions 05, 06 are concerned with mining and quarrying of fossil fuels (coal, lignite, petroleum, gas); divisions 07, 08 concern metal ores, various minerals and quarry products. Some of the technical operations of this section, particularly related to the extraction of hydrocarbons, may also be carried out for third parties by specialised units as an industrial service which is reflected in division 09. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This section excludes:- processing of the extracted materials, see section C (Manufacturing)- usage of the extracted materials without a further transformation for construction purposes, see section F (Construction)- bottling of natural spring and mineral waters at springs and wells, see 11.07- crushing, grinding or otherwise treating certain earths, rocks and minerals not carried on in conjunction with mining and quarrying, see 23.9 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398538
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				05 
			</td>
		

			<td valign='top'>
				B 
			</td>
		

			<td valign='top'>
				Mining of coal and lignite 
			</td>
		

			<td valign='top'>
				05 
			</td>
		

			<td valign='top'>
				This division includes the extraction of solid mineral fuels through underground or open-cast mining and includes operations (e.g. grading, cleaning, compressing and other steps necessary for transportation etc.) leading to a marketable product. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division does not include coking (see 19.10), services incidental to coal or lignite mining (see 09.90) or the manufacture of briquettes (see 19.20). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398539
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				05.1 
			</td>
		

			<td valign='top'>
				05 
			</td>
		

			<td valign='top'>
				Mining of hard coal 
			</td>
		

			<td valign='top'>
				051 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398540
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				05.10 
			</td>
		

			<td valign='top'>
				05.1 
			</td>
		

			<td valign='top'>
				Mining of hard coal 
			</td>
		

			<td valign='top'>
				0510 
			</td>
		

			<td valign='top'>
				This class includes:- mining of hard coal: underground or surface mining, including mining through liquefaction methods- cleaning, sizing, grading, pulverising, compressing etc. of coal to classify, improve quality or facilitate transport or storage 
			</td>
		

			<td valign='top'>
				This class also includes:- recovery of hard coal from culm banks 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- lignite mining, see 05.20- peat digging, see 08.92- support activities for hard coal mining, see 09.90- test drilling for coal mining, see 09.90- coke ovens producing solid fuels, see 19.10- manufacture of hard coal briquettes, see 19.20- work performed to develop or prepare properties for coal mining, see 43.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398541
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				05.2 
			</td>
		

			<td valign='top'>
				05 
			</td>
		

			<td valign='top'>
				Mining of lignite 
			</td>
		

			<td valign='top'>
				052 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398542
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				05.20 
			</td>
		

			<td valign='top'>
				05.2 
			</td>
		

			<td valign='top'>
				Mining of lignite 
			</td>
		

			<td valign='top'>
				0520 
			</td>
		

			<td valign='top'>
				This class includes:- mining of lignite (brown coal): underground or surface mining, including mining through liquefaction methods- washing, dehydrating, pulverising, compressing of lignite to improve quality or facilitate transport or storage 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- hard coal mining, see 05.10- peat digging, see 08.92- support activities for lignite mining, see 09.90- test drilling for coal mining, see 09.90- manufacture of lignite fuel briquettes, see 19.20- work performed to develop or prepare properties for coal mining, see 43.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398543
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				06 
			</td>
		

			<td valign='top'>
				B 
			</td>
		

			<td valign='top'>
				Extraction of crude petroleum and natural gas 
			</td>
		

			<td valign='top'>
				06 
			</td>
		

			<td valign='top'>
				This division includes the production of crude petroleum, the mining and extraction of oil from oil shale and oil sands and the production of natural gas and recovery of hydrocarbon liquids. This division includes the activities of operating and/or developing oil and gas field properties. Such activities may include drilling, completing and equipping wells; operating separators, emulsion breakers, desalting equipment and field gathering lines for crude petroleum; and all other activities in the preparation of oil and gas up to the point of shipment from the producing property. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- oil and gas field services, performed on a fee or contract basis, see 09.10- oil and gas well exploration, see 09.10- test drilling and boring, see 09.10- refining of petroleum products, see 19.20- geophysical, geologic and seismic surveying, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398544
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				06.1 
			</td>
		

			<td valign='top'>
				06 
			</td>
		

			<td valign='top'>
				Extraction of crude petroleum 
			</td>
		

			<td valign='top'>
				061 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398545
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				06.10 
			</td>
		

			<td valign='top'>
				06.1 
			</td>
		

			<td valign='top'>
				Extraction of crude petroleum 
			</td>
		

			<td valign='top'>
				0610 
			</td>
		

			<td valign='top'>
				This class includes:- extraction of crude petroleum oils 
			</td>
		

			<td valign='top'>
				This class also includes:- extraction of bituminous or oil shale and tar sand- production of crude petroleum from bituminous shale and sand- processes to obtain crude oils: decantation, desalting, dehydration, stabilisation etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- support activities for oil and natural gas extraction, see 09.10- oil and gas exploration, see 09.10- manufacture of refined petroleum products, see 19.20- recovery of liquefied petroleum gases in the refining of petroleum, see 19.20- operation of pipelines, see 49.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398546
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				06.2 
			</td>
		

			<td valign='top'>
				06 
			</td>
		

			<td valign='top'>
				Extraction of natural gas 
			</td>
		

			<td valign='top'>
				062 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398547
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				06.20 
			</td>
		

			<td valign='top'>
				06.2 
			</td>
		

			<td valign='top'>
				Extraction of natural gas 
			</td>
		

			<td valign='top'>
				0620 
			</td>
		

			<td valign='top'>
				This class includes:- production of crude gaseous hydrocarbon (natural gas)- extraction of condensates- draining and separation of liquid hydrocarbon fractions- gas desulphurisation 
			</td>
		

			<td valign='top'>
				This class also includes:- mining of hydrocarbon liquids, obtained through liquefaction or pyrolysis 
			</td>
		

			<td valign='top'>
				- Extraction of coal mine methane 
			</td>
		

			<td valign='top'>
				This class excludes:- support activities for oil and natural gas extraction, see 09.10- oil and gas exploration, see 09.10- recovery of liquefied petroleum gases in the refining of petroleum, see 19.20- manufacture of industrial gases, see 20.11- operation of pipelines, see 49.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398548
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				07 
			</td>
		

			<td valign='top'>
				B 
			</td>
		

			<td valign='top'>
				Mining of metal ores 
			</td>
		

			<td valign='top'>
				07 
			</td>
		

			<td valign='top'>
				This division includes mining for metallic minerals (ores), performed through underground or open-cast extraction, seabed mining etc. 
			</td>
		

			<td valign='top'>
				This division also includes:- ore dressing and beneficiating operations, such as crushing, grinding, washing, drying, sintering, calcining or leaching ore, gravity separation or flotation operations 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes:- roasting of iron pyrites, see 20.13- production of aluminium oxide, see 24.42- operation of blast furnaces, see division 24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398549
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				07.1 
			</td>
		

			<td valign='top'>
				07 
			</td>
		

			<td valign='top'>
				Mining of iron ores 
			</td>
		

			<td valign='top'>
				071 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398550
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				07.10 
			</td>
		

			<td valign='top'>
				07.1 
			</td>
		

			<td valign='top'>
				Mining of iron ores 
			</td>
		

			<td valign='top'>
				0710 
			</td>
		

			<td valign='top'>
				This class includes:- mining of ores valued chiefly for iron content- beneficiation and agglomeration of iron ores 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- extraction and preparation of pyrites and pyrrhotite (except roasting), see 08.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398551
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				07.2 
			</td>
		

			<td valign='top'>
				07 
			</td>
		

			<td valign='top'>
				Mining of non-ferrous metal ores 
			</td>
		

			<td valign='top'>
				072 
			</td>
		

			<td valign='top'>
				This group includes the mining of non-ferrous metal ores. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398552
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				07.21 
			</td>
		

			<td valign='top'>
				07.2 
			</td>
		

			<td valign='top'>
				Mining of uranium and thorium ores 
			</td>
		

			<td valign='top'>
				0721 
			</td>
		

			<td valign='top'>
				This class includes:- mining of ores chiefly valued for uranium and thorium content: pitchblende etc.- concentration of such ores- manufacture of yellowcake 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- enrichment of uranium and thorium ores, see 20.13- production of uranium metal from pitchblende or other ores, see 24.46- smelting and refining of uranium, see 24.46 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398553
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				07.29 
			</td>
		

			<td valign='top'>
				07.2 
			</td>
		

			<td valign='top'>
				Mining of other non-ferrous metal ores 
			</td>
		

			<td valign='top'>
				0729 
			</td>
		

			<td valign='top'>
				This class includes:- mining and preparation of ores chiefly valued for non-ferrous metal content:  • aluminium (bauxite), copper, lead, zinc, tin, manganese, chrome, nickel, cobalt, molybdenum, tantalum, vanadium etc.  • precious metals: gold, silver, platinum 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- mining and preparation of uranium and thorium ores, see 07.21- production of aluminium oxide, see 24.42- production of mattes of copper or of nickel, see 24.44, 24.45 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398554
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				08 
			</td>
		

			<td valign='top'>
				B 
			</td>
		

			<td valign='top'>
				Other mining and quarrying 
			</td>
		

			<td valign='top'>
				08 
			</td>
		

			<td valign='top'>
				This division includes extraction from a mine or quarry, but also dredging of alluvial deposits, rock crushing and the use of salt marshes. The products are used most notably in construction (e.g. sands, stones etc.), manufacture of materials (e.g. clay, gypsum, calcium etc.), manufacture of chemicals etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division does not include processing (except crushing, grinding, cutting, cleaning, drying, sorting and mixing) of the minerals extracted. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398555
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				08.1 
			</td>
		

			<td valign='top'>
				08 
			</td>
		

			<td valign='top'>
				Quarrying of stone, sand and clay 
			</td>
		

			<td valign='top'>
				081 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398556
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				08.11 
			</td>
		

			<td valign='top'>
				08.1 
			</td>
		

			<td valign='top'>
				Quarrying of ornamental and building stone, limestone, gypsum, chalk and slate 
			</td>
		

			<td valign='top'>
				0810 
			</td>
		

			<td valign='top'>
				This class includes:- quarrying, rough trimming and sawing of monumental and building stone such as marble, granite, sandstone etc.- breaking and crushing of ornamental and building stone- quarrying, crushing and breaking of limestone- mining of gypsum and anhydrite- mining of chalk and uncalcined dolomite 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- mining of chemical and fertiliser minerals, see 08.91- production of calcined dolomite, see 23.52- cutting, shaping and finishing of stone outside quarries, see 23.70 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398557
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				08.12 
			</td>
		

			<td valign='top'>
				08.1 
			</td>
		

			<td valign='top'>
				Operation of gravel and sand pits; mining of clays and kaolin 
			</td>
		

			<td valign='top'>
				0810 
			</td>
		

			<td valign='top'>
				This class includes:- extraction and dredging of industrial sand, sand for construction and gravel- breaking and crushing of gravel - quarrying of sand- mining of clays, refractory clays and kaolin 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Crushing and breaking of gravel off mine site 
			</td>
		

			<td valign='top'>
				This class excludes:- mining of bituminous sand, see 06.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398558
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				08.9 
			</td>
		

			<td valign='top'>
				08 
			</td>
		

			<td valign='top'>
				Mining and quarrying n.e.c. 
			</td>
		

			<td valign='top'>
				089 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398559
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				08.91 
			</td>
		

			<td valign='top'>
				08.9 
			</td>
		

			<td valign='top'>
				Mining of chemical and fertiliser minerals 
			</td>
		

			<td valign='top'>
				0891 
			</td>
		

			<td valign='top'>
				This class includes:- mining of natural phosphates and natural potassium salts- mining of native sulphur- extraction and preparation of pyrites and pyrrhotite, except roasting- mining of natural barium sulphate and carbonate (barytes and witherite), natural borates, natural magnesium sulphates (kieserite)- mining of earth colours, fluorspar and other minerals valued chiefly as a source of chemicals 
			</td>
		

			<td valign='top'>
				This class also includes:- guano mining 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- extraction of salt, see 08.93- roasting of iron pyrites, see 20.13- manufacture of synthetic fertilisers and nitrogen compounds, see 20.15 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398560
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				08.92 
			</td>
		

			<td valign='top'>
				08.9 
			</td>
		

			<td valign='top'>
				Extraction of peat 
			</td>
		

			<td valign='top'>
				0892 
			</td>
		

			<td valign='top'>
				This class includes:- peat digging- preparation of peat to improve quality or facilitate transport or storage 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- service activities incidental to peat mining, see 09.90- manufacture of peat briquettes, see 19.20- manufacture of potting soil mixtures of peat, natural soil, sands, clays, fertiliser minerals etc., see 20.15- manufacture of articles of peat, see 23.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398561
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				08.93 
			</td>
		

			<td valign='top'>
				08.9 
			</td>
		

			<td valign='top'>
				Extraction of salt 
			</td>
		

			<td valign='top'>
				0893 
			</td>
		

			<td valign='top'>
				This class includes:- extraction of salt from underground including by dissolving and pumping- salt production by evaporation of sea water or other saline waters- crushing, purification and refining of salt by the producer 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- processing of salt into food-grade salt, e.g. iodised salt, see 10.84- potable water production by evaporation of saline water, see 36.00 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398562
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				08.99 
			</td>
		

			<td valign='top'>
				08.9 
			</td>
		

			<td valign='top'>
				Other mining and quarrying n.e.c. 
			</td>
		

			<td valign='top'>
				0899 
			</td>
		

			<td valign='top'>
				This class includes:- mining and quarrying of various minerals and materials:  • abrasive materials, asbestos, siliceous fossil meals, natural graphite, steatite (talc), feldspar etc.  • natural asphalt, asphaltites and asphaltic rock; natural solid bitumen  • gemstones, quartz, mica etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398563
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				09 
			</td>
		

			<td valign='top'>
				B 
			</td>
		

			<td valign='top'>
				Mining support service activities 
			</td>
		

			<td valign='top'>
				09 
			</td>
		

			<td valign='top'>
				This division includes specialised support services incidental to mining provided on a fee or contract basis. It includes exploration services through traditional prospecting methods such as taking core samples and making geological observations as well as drilling, test-drilling or redrilling for oil wells, metallic and non-metallic minerals. Other typical services cover building oil and gas well foundations, cementing oil and gas well casings, cleaning, bailing and swabbing oil and gas wells, draining and pumping mines, overburden removal services at mines, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398564
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				09.1 
			</td>
		

			<td valign='top'>
				09 
			</td>
		

			<td valign='top'>
				Support activities for petroleum and natural gas extraction 
			</td>
		

			<td valign='top'>
				091 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398565
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				09.10 
			</td>
		

			<td valign='top'>
				09.1 
			</td>
		

			<td valign='top'>
				Support activities for petroleum and natural gas extraction 
			</td>
		

			<td valign='top'>
				0910 
			</td>
		

			<td valign='top'>
				This class includes:- oil and gas extraction service activities provided on a fee or contract basis:  • exploration services in connection with petroleum or gas extraction, e.g. traditional prospecting methods, such as making geological observations at prospective sites  • directional drilling and redrilling; &quot;spudding in&quot;; derrick erection in situ, repairing and dismantling; cementing oil and gas well casings; pumping of wells; plugging and abandoning wells etc.  • liquefaction and regasification of natural gas for purpose of transport, done at the mine site  • draining and pumping services, on a fee or contract basis  • test drilling in connection with petroleum or gas extraction 
			</td>
		

			<td valign='top'>
				This class also includes:- oil and gas field fire fighting services 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- service activities performed by operators of oil or gas fields, see 06.10, 06.20- specialised repair of mining machinery, see 33.12- liquefaction and regasification of natural gas for purpose of transport, done off the mine site, see 52.21- geophysical, geologic and seismic surveying, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398566
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				09.9 
			</td>
		

			<td valign='top'>
				09 
			</td>
		

			<td valign='top'>
				Support activities for other mining and quarrying 
			</td>
		

			<td valign='top'>
				099 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398567
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				09.90 
			</td>
		

			<td valign='top'>
				09.9 
			</td>
		

			<td valign='top'>
				Support activities for other mining and quarrying 
			</td>
		

			<td valign='top'>
				0990 
			</td>
		

			<td valign='top'>
				This class includes:- support services on a fee or contract basis, required for mining activities of divisions 05, 07 and 08  • exploration services, e.g. traditional prospecting methods, such as taking core samples and making geological observations at prospective sites  • draining and pumping services, on a fee or contract basis  • test drilling and test hole boring 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Quarries blasting activities 
			</td>
		

			<td valign='top'>
				This class excludes:- operating mines or quarries on a contract or fee basis, see division 05, 07 or 08- specialised repair of mining machinery, see 33.12- geophysical surveying services, on a contract or fee basis, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398568
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				C 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				MANUFACTURING 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				This section includes the physical or chemical transformation of materials, substances, or components into new products, although this cannot be used as the single universal criterion for defining manufacturing (see remark on processing of waste below). The materials, substances, or components transformed are raw materials that are products of agriculture, forestry, fishing, mining or quarrying as well as products of other manufacturing activities. Substantial alteration, renovation or reconstruction of goods is generally considered to be manufacturing.The output of a manufacturing process may be finished in the sense that it is ready for utilisation or consumption, or it may be semi-finished in the sense that it is to become an input for further manufacturing. For example, the output of alumina refining is the input used in the primary production of aluminium; primary aluminium is the input to aluminium wire drawing; and aluminium wire is the input for the manufacture of fabricated wire products.Manufacture of specialised components and parts of, and accessories and attachments to, machinery and equipment is, as a general rule, classified in the same class as the manufacture of the machinery and equipment for which the parts and accessories are intended. Manufacture of unspecialised components and parts of machinery and equipment, e.g. engines, pistons, electric motors, electrical assemblies, valves, gears, roller bearings, is classified in the appropriate class of manufacturing, without regard to the machinery and equipment in which these items may be included. However, making specialised components and accessories by moulding or extruding plastics materials is included in group 22.2.Assembly of the component parts of manufactured products is considered manufacturing. This includes the assembly of manufactured products from either self-produced or purchased components. The recovery of waste, i.e. the processing of waste into secondary raw materials is classified in group 38.3 (Materials recovery). While this may involve physical or chemical transformations, this is not considered to be a part of manufacturing. The primary purpose of these activities is considered to be the treatment or processing of waste and they are therefore classified in Section E (Water supply; sewerage, waste management and remediation activities). However, the manufacture of new final products (as opposed to secondary raw materials) is classified in manufacturing, even if these processes use waste as an input. For example, the production of silver from film waste is considered to be a manufacturing process.Specialised maintenance and repair of industrial, commercial and similar machinery and equipment is, in general, classified in division 33 (Repair, maintenance and installation of machinery and equipment). However, the repair of computers and personal and household goods is classified in division 95 (Repair of computers and personal and household goods), while the repair of motor vehicles is classified in division 45 (Wholesale and retail trade and repair of motor vehicles and motorcycles). The installation of machinery and equipment, when carried out as a specialised activity, is classified in 33.20. Remark: The boundaries of manufacturing and the other sectors of the classification system can be somewhat blurry. As a general rule, the activities in the manufacturing section involve the transformation of materials into new products. Their output is a new product. However, the definition of what constitutes a new product can be somewhat subjective. As clarification, the following activities are considered manufacturing in NACE:- fresh fish processing (oyster shucking, fish filleting), not done on a fishing boat (see 10.20)- milk pasteurising and bottling (see 10.51)- leather converting (see 15.11)- wood preserving (see 16.10)- printing and related activities (see 18.1)- tyre retreading (see 22.11)- ready-mixed concrete production (see 23.63)- electroplating, plating, and metal heat treating (see 25.61)- rebuilding or remanufacture of machinery (e.g. automobile engines, see 29.10)Conversely, there are activities that, although sometimes involving transformation processes, are classified in other sections of NACE; in other words, they are not classified as manufacturing. They include:- logging, classified in section A (Agriculture, forestry and fishing);- beneficiating of agricultural products, classified in section A (Agriculture, forestry and fishing); - preparation of food for immediate consumption on the premises is classified to division 56 (Food and beverage service activities);- beneficiating of ores and other minerals, classified in section B (Mining and quarrying); - construction of structures and fabricating operations performed at the site of construction, classified in section F (Construction);- activities of breaking bulk and redistribution in smaller lots, including packaging, repackaging, or bottling products, such as liquors or chemicals; sorting of scrap; mixing paints to customer order; and cutting metals to customer order; treatment not resulting into a different good is classified to section G (Wholesale and retail trade; repair of motor vehicles and motorcycles). 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398569
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of food products 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				This division includes the processing of the products of agriculture, forestry and fishing into food for humans or animals, and includes the production of various intermediate products that are not directly food products. The activity often generates associated products of greater or lesser value (for example, hides from slaughtering, or oilcake from oil production).This division is organised by activities dealing with different kinds of products: meat, fish, fruit and vegetables, fats and oils, milk products, grain mill products, animal feeds and other food products. Production can be carried out for own account, as well as for third parties, as in custom slaughtering.Some activities are considered manufacturing (for example, those performed in bakeries, pastry shops, and prepared meat shops etc. which sell their own production) even though there is retail sale of the products in the producers' own shop. However, where the processing is minimal and does not lead to a real transformation, the unit is classified to wholesale and retail trade (section G).Preparation of food for immediate consumption on the premises is classified to division 56 (Food and beverage service activities).Production of animal feeds from slaughter waste or by-products is classified in 10.9, while processing food and beverage waste into secondary raw material is classified to 38.3, and disposal of food and beverage waste in 38.21. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division does not include the preparation of meals for immediate consumption, such as in restaurants. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398570
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.1 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Processing and preserving of meat and production of meat products 
			</td>
		

			<td valign='top'>
				101 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398571
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.11 
			</td>
		

			<td valign='top'>
				10.1 
			</td>
		

			<td valign='top'>
				Processing and preserving of meat 
			</td>
		

			<td valign='top'>
				1010 
			</td>
		

			<td valign='top'>
				This class includes:- operation of slaughterhouses engaged in killing, dressing or packing meat: beef, pork, lamb, rabbit, mutton, camel, etc.- production of fresh, chilled or frozen meat, in carcasses- production of fresh, chilled or frozen meat, in cuts 
			</td>
		

			<td valign='top'>
				This class also includes:- slaughtering and processing of whales on land or on specialised vessels- production of hides and skins originating from slaughterhouses, including fellmongery- rendering of lard and other edible fats of animal origin- processing of animal offal- production of pulled wool 
			</td>
		

			<td valign='top'>
				- Drying of pig ears- Rendering of edible fat, from slaughterhouse waste 
			</td>
		

			<td valign='top'>
				This class excludes:- rendering of edible poultry fats, see 10.12- packaging of meat, see 82.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398572
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.12 
			</td>
		

			<td valign='top'>
				10.1 
			</td>
		

			<td valign='top'>
				Processing and preserving of poultry meat 
			</td>
		

			<td valign='top'>
				1010 
			</td>
		

			<td valign='top'>
				This class includes:- operation of slaughterhouses engaged in killing, dressing or packing poultry- production of fresh, chilled or frozen meat in individual portions- rendering of edible poultry fats 
			</td>
		

			<td valign='top'>
				This class also includes:- production of feathers and down 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- packaging of meat, see 82.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398573
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.13 
			</td>
		

			<td valign='top'>
				10.1 
			</td>
		

			<td valign='top'>
				Production of meat and poultry meat products 
			</td>
		

			<td valign='top'>
				1010 
			</td>
		

			<td valign='top'>
				This class includes:- production of dried, salted or smoked meat- production of meat products:  • sausages, salami, puddings, &quot;andouillettes&quot;, saveloys, bolognas, pâtés, rillettes, boiled ham 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of prepared frozen meat and poultry dishes, see 10.85- manufacture of soup containing meat, see 10.89- wholesale trade of meat, see 46.32- packaging of meat, see 82.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398574
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.2 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Processing and preserving of fish, crustaceans and molluscs 
			</td>
		

			<td valign='top'>
				102 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398575
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.20 
			</td>
		

			<td valign='top'>
				10.2 
			</td>
		

			<td valign='top'>
				Processing and preserving of fish, crustaceans and molluscs 
			</td>
		

			<td valign='top'>
				1020 
			</td>
		

			<td valign='top'>
				This class includes:- preparation and preservation of fish, crustaceans and molluscs: freezing, deep-freezing, drying, cooking, smoking, salting, immersing in brine, canning etc.- production of fish, crustacean and mollusc products: fish fillets, roes, caviar, caviar substitutes etc.- production of fishmeal for human consumption or animal feed- production of meals and solubles from fish and other aquatic animals unfit for human consumption 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of vessels engaged only in the processing and preserving of fish- processing of seaweed 
			</td>
		

			<td valign='top'>
				- Fish defrosting, removing heads, gutting and cutting into pieces, and then freezing 
			</td>
		

			<td valign='top'>
				This class excludes:- processing and preserving of fish on vessels engaged in fishing, see 03.11- processing of whales on land or specialised vessels, see 10.11- production of oils and fats from marine material, see 10.41- manufacture of prepared frozen fish dishes, see 10.85- manufacture of fish soups, see 10.89 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398576
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.3 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Processing and preserving of fruit and vegetables 
			</td>
		

			<td valign='top'>
				103 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398577
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.31 
			</td>
		

			<td valign='top'>
				10.3 
			</td>
		

			<td valign='top'>
				Processing and preserving of potatoes 
			</td>
		

			<td valign='top'>
				1030 
			</td>
		

			<td valign='top'>
				This class includes:- processing and preserving of potatoes:  • manufacture of prepared frozen potatoes  • manufacture of dehydrated mashed potatoes  • manufacture of potato snacks  • manufacture of potato crisps  • manufacture of potato flour and meal 
			</td>
		

			<td valign='top'>
				This class also includes:- industrial peeling of potatoes 
			</td>
		

			<td valign='top'>
				- Peeling and cutting of potatoes 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398578
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.32 
			</td>
		

			<td valign='top'>
				10.3 
			</td>
		

			<td valign='top'>
				Manufacture of fruit and vegetable juice 
			</td>
		

			<td valign='top'>
				1030 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of fruit or vegetable juices 
			</td>
		

			<td valign='top'>
				This class also includes:- production of concentrates from fresh fruits and vegetables 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398579
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.39 
			</td>
		

			<td valign='top'>
				10.3 
			</td>
		

			<td valign='top'>
				Other processing and preserving of fruit and vegetables 
			</td>
		

			<td valign='top'>
				1030 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of food consisting chiefly of fruit or vegetables, except ready-made dishes in frozen or canned form- preserving of fruit, nuts or vegetables: freezing, drying, immersing in oil or in vinegar, canning etc.- manufacture of fruit or vegetable food products- manufacture of jams, marmalades and table jellies- roasting of nuts- manufacture of nut foods and pastes 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of perishable prepared foods of fruit and vegetables, such as:  • salads; mixed salads, packaged  • peeled or cut vegetables  • tofu (bean curd) 
			</td>
		

			<td valign='top'>
				- Shelling and peeling of nuts 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of fruit or vegetable juices, see 10.32- manufacture of flour or meal of dried leguminous vegetables, see 10.61- preservation of fruit and nuts in sugar, see 10.82- manufacture of prepared vegetable dishes, see 10.85- manufacture of artificial concentrates, see 10.89 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398580
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.4 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Manufacture of vegetable and animal oils and fats 
			</td>
		

			<td valign='top'>
				104 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of crude and refined oils and fats from vegetable or animal materials, except rendering or refining of lard and other edible animal fats. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398581
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.41 
			</td>
		

			<td valign='top'>
				10.4 
			</td>
		

			<td valign='top'>
				Manufacture of oils and fats 
			</td>
		

			<td valign='top'>
				1040 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of crude vegetable oils: olive oil, soya-bean oil, palm oil, sunflower-seed oil, cotton-seed oil, rape, colza or mustard oil, linseed oil etc.- manufacture of non-defatted flour or meal of oilseeds, oil nuts or oil kernels- manufacture of refined vegetable oils: olive oil, soya-bean oil etc.- processing of vegetable oils: blowing, boiling, dehydration, hydrogenation etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of non-edible animal oils and fats- extraction of fish and marine mammal oils- production of cotton linters, oilcakes and other residual products of oil production 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rendering and refining of lard and other edible animal fats, see 10.11- manufacture of margarine, see 10.42- wet corn milling, see 10.62- manufacture of corn oil, see 10.62- production of essential oils, see 20.53- treatment of oil and fats by chemical processes, see 20.59 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398582
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.42 
			</td>
		

			<td valign='top'>
				10.4 
			</td>
		

			<td valign='top'>
				Manufacture of margarine and similar edible fats 
			</td>
		

			<td valign='top'>
				1040 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of margarine- manufacture of melanges and similar spreads- manufacture of compound cooking fats 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398583
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.5 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Manufacture of dairy products 
			</td>
		

			<td valign='top'>
				105 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398584
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.51 
			</td>
		

			<td valign='top'>
				10.5 
			</td>
		

			<td valign='top'>
				Operation of dairies and cheese making 
			</td>
		

			<td valign='top'>
				1050 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of fresh liquid milk, pasteurised, sterilised, homogenised and/or ultra heat treated- manufacture of milk-based drinks- manufacture of cream from fresh liquid milk, pasteurised, sterilised, homogenised- manufacture of dried or concentrated milk whether or not sweetened- manufacture of milk or cream in solid form- manufacture of butter- manufacture of yoghurt- manufacture of cheese and curd- manufacture of whey- manufacture of casein or lactose 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of raw milk (cattle), see 01.41- production of raw milk (sheep, goats, horses, asses, camels, etc.), see 01.43, 01.44, 01.45- manufacture of non-dairy milk and cheese substitutes, see 10.89 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398585
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.52 
			</td>
		

			<td valign='top'>
				10.5 
			</td>
		

			<td valign='top'>
				Manufacture of ice cream 
			</td>
		

			<td valign='top'>
				1050 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ice cream and other edible ice such as sorbet 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of ice cream parlours, see 56.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398586
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.6 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Manufacture of grain mill products, starches and starch products 
			</td>
		

			<td valign='top'>
				106 
			</td>
		

			<td valign='top'>
				This group includes the milling of flour or meal from grains or vegetables, the milling, cleaning and polishing of rice, as well as the manufacture of flour mixes or doughs from these products. 
			</td>
		

			<td valign='top'>
				Also included in this group are the wet milling of corn and vegetables and the manufacture of starch and starch products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398587
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.61 
			</td>
		

			<td valign='top'>
				10.6 
			</td>
		

			<td valign='top'>
				Manufacture of grain mill products 
			</td>
		

			<td valign='top'>
				1061 
			</td>
		

			<td valign='top'>
				This class includes:- grain milling: production of flour, groats, meal or pellets of wheat, rye, oats, maize (corn) or other cereal grains- rice milling: production of husked, milled, polished, glazed, parboiled or converted rice; production of rice flour- vegetable milling: production of flour or meal of dried leguminous vegetables, of roots or tubers, or of edible nuts- manufacture of cereal breakfast foods- manufacture of flour mixes and prepared blended flour and dough for bread, cakes, biscuits or pancakes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of potato flour and meal, see 10.31- wet corn milling, see 10.62 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398588
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.62 
			</td>
		

			<td valign='top'>
				10.6 
			</td>
		

			<td valign='top'>
				Manufacture of starches and starch products 
			</td>
		

			<td valign='top'>
				1062 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of starches from rice, potatoes, maize etc.- wet corn milling- manufacture of glucose, glucose syrup, maltose, inulin etc.- manufacture of gluten- manufacture of tapioca and tapioca substitutes prepared from starch- manufacture of corn oil 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of lactose (milk sugar), see 10.51- production of cane or beet sugar, see 10.81 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398589
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.7 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Manufacture of bakery and farinaceous products 
			</td>
		

			<td valign='top'>
				107 
			</td>
		

			<td valign='top'>
				This group includes the production of bakery products, macaroni, noodles and similar products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398590
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.71 
			</td>
		

			<td valign='top'>
				10.7 
			</td>
		

			<td valign='top'>
				Manufacture of bread; manufacture of fresh pastry goods and cakes 
			</td>
		

			<td valign='top'>
				1071 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of bakery products:  • bread and rolls  • pastry, cakes, pies, tarts, pancakes, waffles, rolls etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of dry bakery products, see 10.72- manufacture of pastas, see 10.73- heating up of bakery items for immediate consumption, see division 56 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398591
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.72 
			</td>
		

			<td valign='top'>
				10.7 
			</td>
		

			<td valign='top'>
				Manufacture of rusks and biscuits; manufacture of preserved pastry goods and cakes 
			</td>
		

			<td valign='top'>
				1071 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of rusks, biscuits and other dry bakery products- manufacture of preserved pastry goods and cakes- manufacture of snack products (cookies, crackers, pretzels etc.), whether sweet or salted 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of potato snacks, see 10.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398592
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.73 
			</td>
		

			<td valign='top'>
				10.7 
			</td>
		

			<td valign='top'>
				Manufacture of macaroni, noodles, couscous and similar farinaceous products 
			</td>
		

			<td valign='top'>
				1074 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of pastas such as macaroni and noodles, whether or not cooked or stuffed- manufacture of couscous- manufacture of canned or frozen pasta products 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of prepared couscous dishes, see 10.85- manufacture of soup containing pasta, see 10.89 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398593
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Manufacture of other food products 
			</td>
		

			<td valign='top'>
				107 
			</td>
		

			<td valign='top'>
				This group includes the production of sugar and confectionery, prepared meals and dishes, coffee, tea and spices, as well as perishable and specialty food products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398594
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.81 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Manufacture of sugar 
			</td>
		

			<td valign='top'>
				1072 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture or refining of sugar (sucrose) and sugar substitutes from the juice of cane, beet, maple and palm- manufacture of sugar syrups- manufacture of molasses- production of maple syrup and sugar 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of glucose, glucose syrup, maltose, see 10.62 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398595
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.82 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Manufacture of cocoa, chocolate and sugar confectionery 
			</td>
		

			<td valign='top'>
				1073 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of cocoa, cocoa butter, cocoa fat, cocoa oil- manufacture of chocolate and chocolate confectionery- manufacture of sugar confectionery: caramels, cachous, nougats, fondant, white chocolate- manufacture of chewing gum- preserving in sugar of fruit, nuts, fruit peels and other parts of plants- manufacture of confectionery lozenges and pastilles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of sucrose sugar, see 10.81 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398596
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.83 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Processing of tea and coffee 
			</td>
		

			<td valign='top'>
				1079 
			</td>
		

			<td valign='top'>
				This class includes:- decaffeinating and roasting of coffee- production of coffee products:  • ground coffee  • soluble coffee  • extracts and concentrates of coffee- manufacture of coffee substitutes- blending of tea and maté- manufacture of extracts and preparations based on tea or maté- packing of tea including packing in tea-bags 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of herb infusions (mint, vervain, chamomile etc.) 
			</td>
		

			<td valign='top'>
				- Production (filling) of tea-bags and coffee pads 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of inulin, see 10.62- manufacture of spirits, beer, wine and soft drinks, see division 11- preparation of botanical products for pharmaceutical use, see 21.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398597
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.84 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Manufacture of condiments and seasonings 
			</td>
		

			<td valign='top'>
				1079 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of spices, sauces and condiments:  • mayonnaise  • mustard flour and meal  • prepared mustard etc.- manufacture of vinegar 
			</td>
		

			<td valign='top'>
				This class also includes:- processing of salt into food-grade salt, e.g. iodised salt 
			</td>
		

			<td valign='top'>
				- Drying of herbs and spices, using industrial methods, where herbs and spices are lyophilised, frozen and dried in this condition 
			</td>
		

			<td valign='top'>
				This class excludes:- growing of spice crops, see 01.28 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398598
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.85 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Manufacture of prepared meals and dishes 
			</td>
		

			<td valign='top'>
				1075 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of ready-made (i.e. prepared, seasoned and cooked) meals and dishes. These dishes are processed to preserve them, such as in frozen or canned form, and are usually packaged and labelled for re-sale, i.e. this class does not include the preparation of meals for immediate consumption, such as in restaurants. To be considered a dish, these foods have to contain at least two distinct ingredients (except seasonings etc.).This class includes:- manufacture of meat or poultry dishes- manufacture of fish dishes, including fish and chips- manufacture of vegetable dishes- manufacture of frozen or otherwise preserved pizza 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of local and national dishes 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of fresh foods or foods with less than two ingredients, see corresponding class in division 10- manufacture of perishable prepared foods, see 10.89- retail sale of prepared meals and dishes in stores, see 47.11, 47.29- wholesale of prepared meals and dishes, see 46.38- activities of food service contractors, see 56.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398599
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.86 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Manufacture of homogenised food preparations and dietetic food 
			</td>
		

			<td valign='top'>
				1079 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of foods for particular nutritional uses:  • infant formulae  • follow-up milk and other follow-up foods  • baby foods  • low-energy and energy-reduced foods intended for weight control  • dietary foods for special medical purposes  • low-sodium foods, including low-sodium or sodium-free dietary salts  • gluten-free foods  • foods intended to meet the expenditure of intense muscular effort, especially for sportsmen  • foods for persons suffering from carbohydrate metabolism disorders (diabetes) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Preparation of enteral nutrition 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398600
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.89 
			</td>
		

			<td valign='top'>
				10.8 
			</td>
		

			<td valign='top'>
				Manufacture of other food products n.e.c. 
			</td>
		

			<td valign='top'>
				1079 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of soups and broths- manufacture of artificial honey and caramel- manufacture of perishable prepared foods, such as:  • sandwiches  • fresh (uncooked) pizza- manufacture of food supplements and other food products n.e.c. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of yeast- manufacture of extracts and juices of meat, fish, crustaceans or molluscs- manufacture of non-dairy milk and cheese substitutes- manufacture of egg products, egg albumin- manufacture of artificial concentrates 
			</td>
		

			<td valign='top'>
				- Processing (de-crystallisation and filtering) of purchased natural honey 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of perishable prepared foods of fruit and vegetables, see 10.39- manufacture of frozen pizza, see 10.85- manufacture of spirits, beer, wine and soft drinks, see division 11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398601
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				10.9 
			</td>
		

			<td valign='top'>
				10 
			</td>
		

			<td valign='top'>
				Manufacture of prepared animal feeds 
			</td>
		

			<td valign='top'>
				108 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398602
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.91 
			</td>
		

			<td valign='top'>
				10.9 
			</td>
		

			<td valign='top'>
				Manufacture of prepared feeds for farm animals 
			</td>
		

			<td valign='top'>
				1080 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of prepared feeds for farm animals, including concentrated animal feed and feed supplements- preparation of unmixed (single) feeds for farm animals 
			</td>
		

			<td valign='top'>
				This class also includes:- treatment of slaughter waste to produce animal feeds 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of fishmeal for animal feed, see 10.20- production of oilseed cake, see 10.41- activities resulting in by-products usable as animal feed without special treatment, e.g. oilseeds (see 10.41), grain milling residues (see 10.61) etc. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398603
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				10.92 
			</td>
		

			<td valign='top'>
				10.9 
			</td>
		

			<td valign='top'>
				Manufacture of prepared pet foods 
			</td>
		

			<td valign='top'>
				1080 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of prepared feeds for pets, including dogs, cats, birds, fish etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- treatment of slaughter waste to produce animal feeds 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of fishmeal for animal feed, see 10.20- production of oilseed cake, see 10.41- activities resulting in by-products usable as animal feed without special treatment, e.g. oilseeds (see 10.41), grain milling residues (see 10.61) etc. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398604
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				11 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of beverages 
			</td>
		

			<td valign='top'>
				11 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of beverages, such as non-alcoholic beverages and mineral water, manufacture of alcoholic beverages mainly through fermentation, beer and wine, and the manufacture of distilled alcoholic beverages. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- production of fruit and vegetable juices, see 10.32- manufacture of milk-based drinks, see 10.51- manufacture of coffee, tea and mate products, see 10.83 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398605
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				11 
			</td>
		

			<td valign='top'>
				Manufacture of beverages 
			</td>
		

			<td valign='top'>
				110 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398606
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.01 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Distilling, rectifying and blending of spirits 
			</td>
		

			<td valign='top'>
				1101 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of distilled, potable, alcoholic beverages: whisky, brandy, gin, liqueurs etc. - manufacture of drinks mixed with distilled alcoholic beverages- blending of distilled spirits- production of neutral spirits 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of non-distilled alcoholic beverages, see 11.02-11.06- manufacture of synthetic ethyl alcohol, see 20.14- manufacture of ethyl alcohol from fermented materials, see 20.14- merely bottling and labelling, see 46.34 (if performed as part of wholesale) and 82.92 (if performed on a fee or contract basis) 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398607
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.02 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Manufacture of wine from grape 
			</td>
		

			<td valign='top'>
				1102 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of wine- manufacture of sparkling wine- manufacture of wine from concentrated grape must 
			</td>
		

			<td valign='top'>
				This class also includes:- blending, purification and bottling of wine- manufacture of low or non-alcoholic wine 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- merely bottling and labelling, see 46.34 (if performed as part of wholesale) and 82.92 (if performed on a fee or contract basis) 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398608
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.03 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Manufacture of cider and other fruit wines 
			</td>
		

			<td valign='top'>
				1102 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of fermented but not distilled alcoholic beverages: sake, cider, perry and other fruit wines 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of mead and mixed beverages containing fruit wines 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- merely bottling and labelling, see 46.34 (if performed as part of wholesale) and 82.92 (if performed on a fee or contract basis) 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398609
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.04 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Manufacture of other non-distilled fermented beverages 
			</td>
		

			<td valign='top'>
				1102 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of vermouth and the like 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- merely bottling and labelling, see 46.34 (if performed as part of wholesale) and 82.92 (if performed on a fee or contract basis) 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398610
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.05 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Manufacture of beer 
			</td>
		

			<td valign='top'>
				1103 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of malt liquors, such as beer, ale, porter and stout 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of low alcohol or non-alcoholic beer 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398611
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.06 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Manufacture of malt 
			</td>
		

			<td valign='top'>
				1103 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398612
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				11.07 
			</td>
		

			<td valign='top'>
				11.0 
			</td>
		

			<td valign='top'>
				Manufacture of soft drinks; production of mineral waters and other bottled waters 
			</td>
		

			<td valign='top'>
				1104 
			</td>
		

			<td valign='top'>
				This class includes manufacture of non-alcoholic beverages (except non-alcoholic beer and wine):- production of natural mineral waters and other bottled waters- manufacture of soft drinks:  • non-alcoholic flavoured and/or sweetened waters: lemonade, orangeade, cola, fruit drinks, tonic waters etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of fruit and vegetable juice, see 10.32- manufacture of milk-based drinks, see 10.51- manufacture of coffee, tea and maté products, see 10.83- manufacture of alcohol-based drinks, see 11.01, 11.02, 11.03, 11.04, 11.05- manufacture of non-alcoholic wine, see 11.02- manufacture of non-alcoholic beer, see 11.05- manufacture of ice, see 35.30- merely bottling and labelling, see 46.34 (if performed as part of wholesale) and 82.92 (if performed on a fee or contract basis) 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398613
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				12 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of tobacco products 
			</td>
		

			<td valign='top'>
				12 
			</td>
		

			<td valign='top'>
				This division includes the processing of an agricultural product, tobacco, into a form suitable for final consumption. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398614
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				12.0 
			</td>
		

			<td valign='top'>
				12 
			</td>
		

			<td valign='top'>
				Manufacture of tobacco products 
			</td>
		

			<td valign='top'>
				120 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398615
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				12.00 
			</td>
		

			<td valign='top'>
				12.0 
			</td>
		

			<td valign='top'>
				Manufacture of tobacco products 
			</td>
		

			<td valign='top'>
				1200 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of tobacco products and products of tobacco substitutes: cigarettes, fine cut tobacco, cigars, pipe tobacco, chewing tobacco, snuff- manufacture of &quot;homogenised&quot; or &quot;reconstituted&quot; tobacco 
			</td>
		

			<td valign='top'>
				This class also includes:- stemming and redrying of tobacco 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- growing or preliminary processing of tobacco, see 01.15, 01.63 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398616
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				13 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of textiles 
			</td>
		

			<td valign='top'>
				13 
			</td>
		

			<td valign='top'>
				This division includes preparation and spinning of textile fibres as well as textile weaving, finishing of textiles and wearing apparel, manufacture of made-up textile articles, except apparel (e.g. household linen, blankets, rugs, cordage etc.). Growing of natural fibres is covered under division 01, while manufacture of synthetic fibres is a chemical process classified in class 20.60. Manufacture of wearing apparel is covered in division 14. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398617
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				13.1 
			</td>
		

			<td valign='top'>
				13 
			</td>
		

			<td valign='top'>
				Preparation and spinning of textile fibres 
			</td>
		

			<td valign='top'>
				131 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398618
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.10 
			</td>
		

			<td valign='top'>
				13.1 
			</td>
		

			<td valign='top'>
				Preparation and spinning of textile fibres 
			</td>
		

			<td valign='top'>
				1311 
			</td>
		

			<td valign='top'>
				This class includes preparatory operations on textile fibres and the spinning of textile fibres. This can be done from varying raw materials, such as silk, wool, other animal, vegetable or man-made fibres, paper or glass etc.This class includes:- preparatory operations on textile fibres:  • reeling and washing of silk  • degreasing and carbonising of wool and dyeing of wool fleece  • carding and combing of all kinds of animal, vegetable and man-made fibres- spinning and manufacture of yarn or thread for weaving or sewing, for the trade or for further processing  • scutching of flax  • texturising, twisting, folding, cabling and dipping of synthetic or artificial filament yarns 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of paper yarn 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- preparatory operations carried out in combination with agriculture, see division 01- retting of plants bearing vegetable textile fibres (jute, flax, coir etc.), see 01.16- cotton ginning, see 01.63- manufacture of synthetic or artificial fibres and tows, manufacture of single yarns (including high-tenacity yarn and yarn for carpets) of synthetic or artificial fibres, see 20.60- manufacture of glass fibres, see 23.14 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398619
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				13.2 
			</td>
		

			<td valign='top'>
				13 
			</td>
		

			<td valign='top'>
				Weaving of textiles 
			</td>
		

			<td valign='top'>
				131 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398620
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.20 
			</td>
		

			<td valign='top'>
				13.2 
			</td>
		

			<td valign='top'>
				Weaving of textiles 
			</td>
		

			<td valign='top'>
				1312 
			</td>
		

			<td valign='top'>
				This class includes weaving of textiles. This can be done from varying raw materials, such as silk, wool, other animal, vegetable or man-made fibres, paper or glass etc.This class includes:- manufacture of broad woven cotton-type, woollen-type, worsted-type or silk-type fabrics, including from mixtures or artificial or synthetic yarns (polypropylene etc.)- manufacture of other broad woven fabrics, using flax, ramie, hemp, jute, bast fibres and special yarns 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of woven pile or chenille fabrics, terry towelling, gauze etc.- manufacture of woven fabrics of glass fibres- manufacture of woven carbon and aramid threads- manufacture of imitation fur by weaving 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of knitted and crocheted fabrics, see 13.91- manufacture of textile floor coverings, see 13.93- manufacture of non-woven fabrics, see 13.95- manufacture of narrow fabrics, see 13.96- manufacture of felts, see 13.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398621
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				13.3 
			</td>
		

			<td valign='top'>
				13 
			</td>
		

			<td valign='top'>
				Finishing of textiles 
			</td>
		

			<td valign='top'>
				131 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398622
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.30 
			</td>
		

			<td valign='top'>
				13.3 
			</td>
		

			<td valign='top'>
				Finishing of textiles 
			</td>
		

			<td valign='top'>
				1313 
			</td>
		

			<td valign='top'>
				This class includes finishing of textiles and wearing apparel, i.e. bleaching, dyeing, dressing and similar activities.This class includes:- bleaching and dyeing of textile fibres, yarns, fabrics and textile articles, including wearing apparel- dressing, drying, steaming, shrinking, mending, sanforising, mercerising of textiles and textile articles, including wearing apparel 
			</td>
		

			<td valign='top'>
				This class also includes:- bleaching of jeans- pleating and similar work on textiles- waterproofing, coating, rubberising, or impregnating purchased garments 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of textile fabric impregnated, coated, covered or laminated with rubber, where rubber is the chief constituent, see 22.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398623
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				13 
			</td>
		

			<td valign='top'>
				Manufacture of other textiles 
			</td>
		

			<td valign='top'>
				139 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of products produced from textiles, except wearing apparel, such as made-up textile articles, carpets and rugs, rope, narrow woven fabrics, trimmings etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398624
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.91 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of knitted and crocheted fabrics 
			</td>
		

			<td valign='top'>
				1391 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture and processing of knitted or crocheted fabrics:  • pile and terry fabrics  • net and window furnishing type fabrics knitted on Raschel or similar machines  • other knitted or crocheted fabrics 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of imitation fur by knitting 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of net and window furnishing type fabrics of lace knitted on Raschel or similar machines, see 13.99- manufacture of knitted and crocheted apparel, see 14.39 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398625
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.92 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of made-up textile articles, except apparel 
			</td>
		

			<td valign='top'>
				1392 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture, of made-up articles of any textile material, including of knitted or crocheted fabrics:  • blankets, including travelling rugs  • bed, table, toilet or kitchen linen  • quilts, eiderdowns, cushions, pouffes, pillows, sleeping bags etc.- manufacture of made-up furnishing articles:  • curtains, valances, blinds, bedspreads, furniture or machine covers etc.  • tarpaulins, tents, camping goods, sails, sunblinds, loose covers for cars, machines or furniture etc.  • flags, banners, pennants etc.  • dust cloths, dishcloths and similar articles, life jackets, parachutes etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of the textile part of electric blankets- manufacture of hand-woven tapestries 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of textile articles for technical use, see 13.96 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398626
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.93 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of carpets and rugs 
			</td>
		

			<td valign='top'>
				1393 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of textile floor coverings:  • carpets, rugs and mats, tiles 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of needle-loom felt floor coverings 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of mats and matting of plaiting materials, see 16.29- manufacture of floor coverings of cork, see 16.29- manufacture of resilient floor coverings, such as vinyl, linoleum, see 22.23 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398627
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.94 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of cordage, rope, twine and netting 
			</td>
		

			<td valign='top'>
				1394 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of twine, cordage, rope and cables of textile fibres or strip or the like, whether or not impregnated, coated, covered or sheathed with rubber or plastics- manufacture of knotted netting of twine, cordage or rope- manufacture of products of rope or netting: fishing nets, ships' fenders, unloading cushions, loading slings, rope or cable fitted with metal rings etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of hairnets, see 14.19- manufacture of wire rope, see 25.93- manufacture of landing nets for sports fishing, see 32.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398628
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.95 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of non-wovens and articles made from non-wovens, except apparel 
			</td>
		

			<td valign='top'>
				1399 
			</td>
		

			<td valign='top'>
				This class includes all activities related to the manufacture of textiles or textile products, not specified elsewhere in division 13 or 14, involving a large number of processes and a great variety of goods produced. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398629
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.96 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of other technical and industrial textiles 
			</td>
		

			<td valign='top'>
				1399 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of narrow woven fabrics, including fabrics consisting of warp without weft assembled by means of an adhesive- manufacture of labels, badges etc.- manufacture of ornamental trimmings: braids, tassels, pompons etc.- manufacture of fabrics impregnated, coated, covered or laminated with plastics- manufacture of metallised yarn or metallised gimped yarn, rubber thread and cord covered with textile material, textile yarn or strip covered, impregnated, coated or sheathed with rubber or plastics- manufacture of tyre cord fabric of high-tenacity man-made yarn- manufacture of other treated or coated fabrics: buckram and similar stiffened textile fabrics, fabrics coated with gum or amylaceous substances- manufacture of diverse textile articles: textile wicks, incandescent gas mantles and tubular gas- manufacture of mantle fabric, hosepiping, transmission or conveyor belts or belting (whether or not reinforced with metal or other material), bolting cloth, straining cloth- manufacture of automotive trimmings- manufacture of artists' canvas boards and tracing cloth 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of transmission or conveyor belts of textile fabric, yarn or cord impregnated, coated, covered or laminated with rubber, where rubber is the chief constituent, see 22.19- manufacture of plates or sheets of cellular rubber or plastic combined with textiles for reinforcing purposes only, see 22.19, 22.21- manufacture of cloth of woven metal wire, see 25.93 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398630
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				13.99 
			</td>
		

			<td valign='top'>
				13.9 
			</td>
		

			<td valign='top'>
				Manufacture of other textiles n.e.c. 
			</td>
		

			<td valign='top'>
				1399 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of felt- manufacture of tulles and other net fabrics, and of lace and embroidery, in the piece, in strips or in motifs- manufacture of pressure sensitive cloth-tape- manufacture of shoe-lace, of textiles- manufacture of powder puffs and mitts 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of needle-loom felt floor coverings, see 13.93- manufacture of textile wadding and articles of wadding: sanitary towels, tampons etc., see 17.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398631
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				14 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of wearing apparel 
			</td>
		

			<td valign='top'>
				14 
			</td>
		

			<td valign='top'>
				This division includes all tailoring (ready-to-wear or made-to-measure), in all materials (e.g. leather, fabric, knitted and crocheted fabrics etc.), of all items of clothing (e.g. outerwear, underwear for men, women or children; work, city or casual clothing etc.) and accessories. There is no distinction made between clothing for adults and clothing for children, or between modern and traditional clothing. 
			</td>
		

			<td valign='top'>
				Division 14 also includes the fur industry (fur skins and wearing apparel). 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398632
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				14.1 
			</td>
		

			<td valign='top'>
				14 
			</td>
		

			<td valign='top'>
				Manufacture of wearing apparel, except fur apparel 
			</td>
		

			<td valign='top'>
				141 
			</td>
		

			<td valign='top'>
				This group includes manufacture of wearing apparel. The material used may be of any kind and may be coated, impregnated or rubberised. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398633
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.11 
			</td>
		

			<td valign='top'>
				14.1 
			</td>
		

			<td valign='top'>
				Manufacture of leather clothes 
			</td>
		

			<td valign='top'>
				1410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of wearing apparel made of leather or composition leather including leather industrial work accessories as welder's leather aprons 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of fur wearing apparel, see 14.20- manufacture of leather sports gloves and sports headgear, see 32.30- manufacture of fire resistant and protective safety clothing, see 32.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398634
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.12 
			</td>
		

			<td valign='top'>
				14.1 
			</td>
		

			<td valign='top'>
				Manufacture of workwear 
			</td>
		

			<td valign='top'>
				1410 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of footwear, see 15.20- manufacture of fire resistant and protective safety clothing, see 32.99- repair of wearing apparel, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398635
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.13 
			</td>
		

			<td valign='top'>
				14.1 
			</td>
		

			<td valign='top'>
				Manufacture of other outerwear 
			</td>
		

			<td valign='top'>
				1410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of other outerwear made of woven, knitted or crocheted fabric, non-wovens etc. for men, women and children:  • coats, suits, ensembles, jackets, trousers, skirts etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- custom tailoring- manufacture of parts of the products listed 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of wearing apparel of fur skins (except headgear), see 14.20- manufacture of wearing apparel of rubber or plastics not assembled by stitching but merely sealed together, see 22.19, 22.29- manufacture of fire resistant and protective safety clothing, see 32.99- repair of wearing apparel, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398636
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.14 
			</td>
		

			<td valign='top'>
				14.1 
			</td>
		

			<td valign='top'>
				Manufacture of underwear 
			</td>
		

			<td valign='top'>
				1410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of underwear and nightwear made of woven, knitted or crocheted fabric, lace etc. for men, women and children:  • shirts, T-shirts, underpants, briefs, pyjamas, nightdresses, dressing gowns, blouses, slips, brassieres, corsets etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- repair of wearing apparel, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398637
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.19 
			</td>
		

			<td valign='top'>
				14.1 
			</td>
		

			<td valign='top'>
				Manufacture of other wearing apparel and accessories 
			</td>
		

			<td valign='top'>
				1410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of babies' garments, tracksuits, ski suits, swimwear etc.- manufacture of hats and caps- manufacture of other clothing accessories: gloves, belts, shawls, ties, cravats, hairnets etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of headgear of fur skins- manufacture of footwear of textile material without applied soles- manufacture of parts of the products listed 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of sports headgear, see 32.30- manufacture of safety headgear (except sports headgear), see 32.99- manufacture of fire resistant and protective safety clothing, see 32.99- repair of wearing apparel, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398638
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				14.2 
			</td>
		

			<td valign='top'>
				14 
			</td>
		

			<td valign='top'>
				Manufacture of articles of fur 
			</td>
		

			<td valign='top'>
				142 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398639
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.20 
			</td>
		

			<td valign='top'>
				14.2 
			</td>
		

			<td valign='top'>
				Manufacture of articles of fur 
			</td>
		

			<td valign='top'>
				1420 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of articles made of fur skins:  • fur wearing apparel and clothing accessories  • assemblies of fur skins such as &quot;dropped&quot; fur skins, plates, mats, strips etc.  • diverse articles of fur skins: rugs, unstuffed pouffes, industrial polishing cloths 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of raw fur skins, see 01.4, 01.70- production of raw hides and skins, see 10.11- manufacture of imitation furs (long-hair cloth obtained by weaving or knitting), see 13.20, 13.91- manufacture of fur hats, see 14.19- manufacture of apparel trimmed with fur, see 14.19- dressing and dyeing of fur, see 15.11- manufacture of boots or shoes containing fur parts, see 15.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398640
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				14.3 
			</td>
		

			<td valign='top'>
				14 
			</td>
		

			<td valign='top'>
				Manufacture of knitted and crocheted apparel 
			</td>
		

			<td valign='top'>
				143 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398641
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.31 
			</td>
		

			<td valign='top'>
				14.3 
			</td>
		

			<td valign='top'>
				Manufacture of knitted and crocheted hosiery 
			</td>
		

			<td valign='top'>
				1430 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of hosiery, including socks, tights and pantyhose 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398642
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				14.39 
			</td>
		

			<td valign='top'>
				14.3 
			</td>
		

			<td valign='top'>
				Manufacture of other knitted and crocheted apparel 
			</td>
		

			<td valign='top'>
				1430 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of knitted or crocheted wearing apparel and other made-up articles directly into shape: pullovers, cardigans, jerseys, waistcoats and similar articles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of knitted and crocheted fabrics, see 13.91- manufacture of hosiery, see 14.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398643
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				15 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of leather and related products 
			</td>
		

			<td valign='top'>
				15 
			</td>
		

			<td valign='top'>
				This division includes dressing and dyeing of fur and the transformation of hides into leather by tanning or curing and fabricating the leather into products for final consumption. 
			</td>
		

			<td valign='top'>
				It also includes the manufacture of similar products from other materials (imitation leathers or leather substitutes), such as rubber footwear, textile luggage etc. The products made from leather substitutes are included here, since they are made in ways similar to those in which leather products are made (e.g. luggage) and are often produced in the same unit. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398644
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				15.1 
			</td>
		

			<td valign='top'>
				15 
			</td>
		

			<td valign='top'>
				Tanning and dressing of leather; manufacture of luggage, handbags, saddlery and harness; dressing and dyeing of fur 
			</td>
		

			<td valign='top'>
				151 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of leather and fur and products thereof. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398645
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				15.11 
			</td>
		

			<td valign='top'>
				15.1 
			</td>
		

			<td valign='top'>
				Tanning and dressing of leather; dressing and dyeing of fur 
			</td>
		

			<td valign='top'>
				1511 
			</td>
		

			<td valign='top'>
				This class includes:- tanning, dyeing and dressing of hides and skins- manufacture of chamois dressed, parchment dressed, patent or metallised leathers- manufacture of composition leather- scraping, shearing, plucking, currying, tanning, bleaching and dyeing of fur skins and hides with the hair on 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of hides and skins as part of ranching, see 01.4- production of hides and skins as part of slaughtering, see 10.11- manufacture of leather apparel, see 14.11- manufacture of imitation leather not based on natural leather, see 22.19, 22.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398646
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				15.12 
			</td>
		

			<td valign='top'>
				15.1 
			</td>
		

			<td valign='top'>
				Manufacture of luggage, handbags and the like, saddlery and harness 
			</td>
		

			<td valign='top'>
				1512 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of luggage, handbags and the like, of leather, composition leather or any other material, such as plastic sheeting, textile materials, vulcanised fibre or paperboard, where the same technology is used as for leather- manufacture of saddlery and harness- manufacture of non-metallic watch bands (e.g. fabric, leather, plastic)- manufacture of diverse articles of leather or composition leather: driving belts, packings etc.- manufacture of shoe-lace, of leather- manufacture of horse whips and riding crops 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of leather wearing apparel, see 14.11- manufacture of leather gloves and hats, see 14.19- manufacture of footwear, see 15.20- manufacture of saddles for bicycles, see 30.92- manufacture of precious metal watch bands, see 32.12- manufacture of non-precious metal watch bands, see 32.13- manufacture of linemen's safety belts and other belts for occupational use, see 32.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398647
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				15.2 
			</td>
		

			<td valign='top'>
				15 
			</td>
		

			<td valign='top'>
				Manufacture of footwear 
			</td>
		

			<td valign='top'>
				152 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398648
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				15.20 
			</td>
		

			<td valign='top'>
				15.2 
			</td>
		

			<td valign='top'>
				Manufacture of footwear 
			</td>
		

			<td valign='top'>
				1520 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of footwear for all purposes, of any material, by any process, including moulding (see below for exceptions)- manufacture of leather parts of footwear: manufacture of uppers and parts of uppers, outer and inner soles, heels etc.- manufacture of gaiters, leggings and similar articles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of footwear of textile material without applied soles, see 14.19- manufacture of wooden shoe parts (e.g. heels and lasts), see 16.29- manufacture of rubber boot and shoe heels and soles and other rubber footwear parts, see 22.19- manufacture of plastic footwear parts, see 22.29- manufacture of ski-boots, see 32.30- manufacture of orthopaedic shoes, see 32.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398649
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				16 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of wood and of products of wood and cork, except furniture; manufacture of articles of straw and plaiting materials 
			</td>
		

			<td valign='top'>
				16 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of wood products, such as lumber, plywood, veneers, wood containers, wood flooring, wood trusses, and prefabricated wood buildings. The production processes include sawing, planing, shaping, laminating, and assembling of wood products starting from logs that are cut into bolts, or lumber that may then be cut further, or shaped by lathes or other shaping tools. The lumber or other transformed wood shapes may also be subsequently planed or smoothed, and assembled into finished products, such as wood containers.With the exception of sawmilling, this division is subdivided mainly based on the specific products manufactured. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division does not include the manufacture of furniture (31.0), or the installation of wooden fittings and the like (43.32, 43.33, 43.39). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398650
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				16.1 
			</td>
		

			<td valign='top'>
				16 
			</td>
		

			<td valign='top'>
				Sawmilling and planing of wood 
			</td>
		

			<td valign='top'>
				161 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398651
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				16.10 
			</td>
		

			<td valign='top'>
				16.1 
			</td>
		

			<td valign='top'>
				Sawmilling and planing of wood 
			</td>
		

			<td valign='top'>
				1610 
			</td>
		

			<td valign='top'>
				This class includes:- sawing, planing and machining of wood- slicing, peeling or chipping logs- manufacture of wooden railway sleepers- manufacture of unassembled wooden flooring- manufacture of wood wool, wood flour, chips, particles 
			</td>
		

			<td valign='top'>
				This class also includes:- drying of wood- impregnation or chemical treatment of wood with preservatives or other materials 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- logging and production of wood in the rough, see 02.20- manufacture of veneer sheets thin enough for use in plywood, boards and panels, see 16.21- manufacture of shingles and shakes, beadings and mouldings, see 16.23- manufacture of fire logs or pressed wood, see 16.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398652
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				16.2 
			</td>
		

			<td valign='top'>
				16 
			</td>
		

			<td valign='top'>
				Manufacture of products of wood, cork, straw and plaiting materials 
			</td>
		

			<td valign='top'>
				162 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of products of wood, cork, straw or plaiting materials, including basic shapes as well as assembled products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398653
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				16.21 
			</td>
		

			<td valign='top'>
				16.2 
			</td>
		

			<td valign='top'>
				Manufacture of veneer sheets and wood-based panels 
			</td>
		

			<td valign='top'>
				1621 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of veneer sheets thin enough to be used for veneering, making plywood or other purposes:  • smoothed, dyed, coated, impregnated, reinforced (with paper or fabric backing)  • made in the form of motifs- manufacture of plywood, veneer panels and similar laminated wood boards and sheets- manufacture of oriented strand board (OSB) and other particle board- manufacture of medium density fibreboard (MDF) and other fibreboard - manufacture of densified wood- manufacture of glue laminated wood, laminated veneer wood 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398654
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				16.22 
			</td>
		

			<td valign='top'>
				16.2 
			</td>
		

			<td valign='top'>
				Manufacture of assembled parquet floors 
			</td>
		

			<td valign='top'>
				1622 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of wooden parquet floor blocks, strips etc., assembled into panels 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of unassembled wooden floors, see 16.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398655
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				16.23 
			</td>
		

			<td valign='top'>
				16.2 
			</td>
		

			<td valign='top'>
				Manufacture of other builders' carpentry and joinery 
			</td>
		

			<td valign='top'>
				1622 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of wooden goods intended to be used primarily in the construction industry:  • beams, rafters, roof struts  • glue-laminated and metal connected, prefabricated wooden roof trusses  • doors, windows, shutters and their frames, whether or not containing metal fittings, such as hinges, locks etc.  • stairs, railings  • wooden beadings and mouldings, shingles and shakes- manufacture of prefabricated buildings, or elements thereof, predominantly of wood, e.g. saunas- manufacture of mobile homes- manufacture of wood partitions (except free standing) 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of kitchen cabinets, bookcases, wardrobes etc., see 31.01, 31.02, 31.09- manufacture of wood partitions, free standing, see 31.01, 31.02, 31.09 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398656
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				16.24 
			</td>
		

			<td valign='top'>
				16.2 
			</td>
		

			<td valign='top'>
				Manufacture of wooden containers 
			</td>
		

			<td valign='top'>
				1623 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of packing cases, boxes, crates, drums and similar packings of wood- manufacture of pallets, box pallets and other load boards of wood- manufacture of barrels, vats, tubs and other coopers' products of wood- manufacture of wooden cable-drums 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of luggage, see 15.12- manufacture of cases of plaiting material, see 16.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398657
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				16.29 
			</td>
		

			<td valign='top'>
				16.2 
			</td>
		

			<td valign='top'>
				Manufacture of other products of wood; manufacture of articles of cork, straw and plaiting materials 
			</td>
		

			<td valign='top'>
				1629 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of various wood products:  • wooden handles and bodies for tools, brooms, brushes  • wooden boot or shoe lasts and trees, clothes hangers  • household utensils and kitchenware of wood  • wooden statuettes and ornaments, wood marquetry, inlaid wood  • wooden cases for jewellery, cutlery and similar articles  • wooden spools, cops, bobbins, sewing thread reels and similar articles of turned wood  • other articles of wood- natural cork processing, manufacture of agglomerated cork- manufacture of articles of natural or agglomerated cork, including floor coverings- manufacture of plaits and products of plaiting materials: mats, matting, screens, cases etc.- manufacture of basket-ware and wickerwork- manufacture of fire logs and pellets for energy, made of pressed wood or substitute materials like coffee or soybean grounds- manufacture of wooden mirror and picture frames- manufacture of frames for artists' canvases- manufacture of wooden shoe parts (e.g. heels and lasts)- manufacture of handles for umbrellas, canes and similar- manufacture of blocks for the manufacture of smoking pipes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of mats or matting of textile materials, see 13.92- manufacture of luggage, see 15.12- manufacture of wooden footwear, see 15.20- manufacture of matches, see 20.51- manufacture of clock cases, see 26.52- manufacture of wooden spools and bobbins that are part of textile machinery, see 28.94- manufacture of furniture, see 31.0- manufacture of wooden toys, see 32.40- manufacture of brushes and brooms, see 32.91- manufacture of coffins, see 32.99- manufacture of cork life preservers, see 32.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398658
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				17 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of paper and paper products 
			</td>
		

			<td valign='top'>
				17 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of pulp, paper and converted paper products. The manufacture of these products is grouped together because they constitute a series of vertically connected processes. More than one activity is often carried out in a single unit. There are essentially three activities: The manufacture of pulp involves separating the cellulose fibres from other matter in wood, or dissolving and de-inking of used paper, and mixing in small amounts of reagents to reinforce the binding of the fibres. The manufacture of paper involves releasing pulp onto a moving wire mesh so as to form a continuous sheet. Converted paper products are made from paper and other materials by various techniques. The paper articles may be printed (e.g. wallpaper, gift wrap etc.), as long as the printing of information is not the main purpose.The production of pulp, paper and paperboard in bulk is included in group 17.1, while the remaining classes include the production of further-processed paper and paper products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398659
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				17.1 
			</td>
		

			<td valign='top'>
				17 
			</td>
		

			<td valign='top'>
				Manufacture of pulp, paper and paperboard 
			</td>
		

			<td valign='top'>
				170 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398660
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.11 
			</td>
		

			<td valign='top'>
				17.1 
			</td>
		

			<td valign='top'>
				Manufacture of pulp 
			</td>
		

			<td valign='top'>
				1701 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of bleached, semi-bleached or unbleached paper pulp by mechanical, chemical (dissolving or non-dissolving) or semi-chemical processes- manufacture of cotton-linters pulp- removal of ink and manufacture of pulp from waste paper 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398661
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.12 
			</td>
		

			<td valign='top'>
				17.1 
			</td>
		

			<td valign='top'>
				Manufacture of paper and paperboard 
			</td>
		

			<td valign='top'>
				1701 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of paper and paperboard intended for further industrial processing 
			</td>
		

			<td valign='top'>
				This class also includes:- further processing of paper and paperboard:  • coating, covering and impregnation of paper and paperboard  • manufacture of crêped or crinkled paper  • manufacture of laminates and foils, if laminated with paper or paperboard- manufacture of handmade paper- manufacture of newsprint and other printing or writing paper- manufacture of cellulose wadding and webs of cellulose fibres- manufacture of carbon paper or stencil paper in rolls or large sheets 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of corrugated paper and paperboard, see 17.21- manufacture of further-processed articles of paper, paperboard or pulp, see 17.22, 17.23, 17.24, 17.29- manufacture of coated or impregnated paper, where the coating or impregnant is the main ingredient, see class in which the manufacture of the coating or impregnant is classified- manufacture of abrasive paper, see 23.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398662
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				17.2 
			</td>
		

			<td valign='top'>
				17 
			</td>
		

			<td valign='top'>
				Manufacture of articles of paper and paperboard 
			</td>
		

			<td valign='top'>
				170 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398663
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.21 
			</td>
		

			<td valign='top'>
				17.2 
			</td>
		

			<td valign='top'>
				Manufacture of corrugated paper and paperboard and of containers of paper and paperboard 
			</td>
		

			<td valign='top'>
				1702 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of corrugated paper and paperboard- manufacture of containers of corrugated paper or paperboard- manufacture of folding paperboard containers- manufacture of containers of solid board- manufacture of other containers of paper and paperboard- manufacture of sacks and bags of paper- manufacture of office box files and similar articles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of envelopes, see 17.23- manufacture of moulded or pressed articles of paper pulp (e.g. boxes for packing eggs, moulded pulp paper plates), see 17.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398664
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.22 
			</td>
		

			<td valign='top'>
				17.2 
			</td>
		

			<td valign='top'>
				Manufacture of household and sanitary goods and of toilet requisites 
			</td>
		

			<td valign='top'>
				1709 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of household and personal hygiene paper and cellulose wadding products:  • cleansing tissues  • handkerchiefs, towels, serviettes  • toilet paper  • sanitary towels and tampons, napkins and napkin liners for babies  • cups, dishes and trays- manufacture of textile wadding and articles of wadding: sanitary towels, tampons etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of cellulose wadding, see 17.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398665
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.23 
			</td>
		

			<td valign='top'>
				17.2 
			</td>
		

			<td valign='top'>
				Manufacture of paper stationery 
			</td>
		

			<td valign='top'>
				1709 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of printing and writing paper ready for use- manufacture of computer printout paper ready for use- manufacture of self-copy paper ready for use- manufacture of duplicator stencils and carbon paper ready for use- manufacture of gummed or adhesive paper ready for use- manufacture of envelopes and letter-cards- manufacture of educational and commercial stationery (notebooks, binders, registers, accounting books, business forms etc.), when the printed information is not the main characteristic- manufacture of boxes, pouches, wallets and writing compendiums containing an assortment of paper stationery 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- printing on paper products, see 18.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398666
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.24 
			</td>
		

			<td valign='top'>
				17.2 
			</td>
		

			<td valign='top'>
				Manufacture of wallpaper 
			</td>
		

			<td valign='top'>
				1709 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of wallpaper and similar wall coverings, including vinyl-coated and textile wallpaper 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of paper or paperboard in bulk, see 17.12- manufacture of plastic wallpaper, see 22.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398667
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				17.29 
			</td>
		

			<td valign='top'>
				17.2 
			</td>
		

			<td valign='top'>
				Manufacture of other articles of paper and paperboard 
			</td>
		

			<td valign='top'>
				1709 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of labels- manufacture of filter paper and paperboard- manufacture of paper and paperboard bobbins, spools, cops etc.- manufacture of egg trays and other moulded pulp packaging products etc.- manufacture of paper novelties- manufacture of paper or paperboard cards for use on Jacquard machines 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of playing cards, see 32.40- manufacture of games and toys of paper or paperboard, see 32.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398668
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				18 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Printing and reproduction of recorded media 
			</td>
		

			<td valign='top'>
				18 
			</td>
		

			<td valign='top'>
				This division includes printing of products, such as newspapers, books, periodicals, business forms, greeting cards, and other materials, and associated support activities, such as bookbinding, plate-making services, and data imaging. The support activities included here are an integral part of the printing industry, and a product (a printing plate, a bound book, or a computer disk or file) that is an integral part of the printing industry is almost always provided by these operations.Processes used in printing include a variety of methods for transferring an image from a plate, screen or computer file to a medium, such as paper, plastics, metal, textile articles, or wood. The most prominent of these methods entails the transfer of the image from a plate or screen to the medium through lithographic, gravure, screen or flexographic printing. Often a computer file is used to directly ''drive'' the printing mechanism to create the image or electrostatic and other types of equipment (digital or non-impact printing).Though printing and publishing can be carried out by the same unit (a newspaper, for example), it is less and less the case that these distinct activities are carried out in the same physical location. 
			</td>
		

			<td valign='top'>
				This division also includes the reproduction of recorded media, such as compact discs, video recordings, software on discs or tapes, records etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes publishing activities (see section J). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398669
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				18.1 
			</td>
		

			<td valign='top'>
				18 
			</td>
		

			<td valign='top'>
				Printing and service activities related to printing 
			</td>
		

			<td valign='top'>
				181 
			</td>
		

			<td valign='top'>
				This group includes printing of products, such as newspapers, books, periodicals, business forms, greeting cards, and other materials, and associated support activities, such as bookbinding, plate-making services, and data imaging. Printing can be done using various techniques and on different materials. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398670
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				18.11 
			</td>
		

			<td valign='top'>
				18.1 
			</td>
		

			<td valign='top'>
				Printing of newspapers 
			</td>
		

			<td valign='top'>
				1811 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- printing of other periodicals, appearing at least four times a week 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- publishing of printed matter, see 58.1- photocopying of documents, see 82.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398671
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				18.12 
			</td>
		

			<td valign='top'>
				18.1 
			</td>
		

			<td valign='top'>
				Other printing 
			</td>
		

			<td valign='top'>
				1811 
			</td>
		

			<td valign='top'>
				This class includes:- printing of magazines and other periodicals, appearing less than four times a week- printing of books and brochures, music and music manuscripts, maps, atlases, posters, advertising catalogues, prospectuses and other printed advertising, postage stamps, taxation stamps, documents of title, cheques and other security papers, smart cards, albums, diaries, calendars and other commercial printed matter, personal stationery and other printed matter by letterpress, offset, photogravure, flexographic, screen printing and other printing presses, duplication machines, computer printers, embossers etc., including quick printing- printing directly onto textiles, plastic, glass, metal, wood and ceramicsThe material printed is typically copyrighted. 
			</td>
		

			<td valign='top'>
				This class also includes:- printing on labels or tags (lithographic, gravure printing, flexographic printing, other) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of stationery (notebooks, binders, registers, accounting books, business forms etc.), when the printed information is not the main characteristic, see 17.23- publishing of printed matter, see 58.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398672
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				18.13 
			</td>
		

			<td valign='top'>
				18.1 
			</td>
		

			<td valign='top'>
				Pre-press and pre-media services 
			</td>
		

			<td valign='top'>
				1812 
			</td>
		

			<td valign='top'>
				This class includes:- composing, typesetting, phototypesetting, pre-press data input including scanning and optical character recognition, electronic make-up- preparation of data files for multi-media (printing on paper, CD-ROM, Internet) applications- plate-making services including image setting and plate setting (for the printing processes letterpress and offset)- cylinder preparation: engraving or etching of cylinders for gravure printing- plate processing: &quot;computer to plate&quot; CTP (also photopolymer plates) - preparation of plates and dies for relief stamping or printing- preparation of:  • artistic works of technical character, such as preparation of lithographic stones and wood blocks  • presentation media, e.g. overhead foils and other forms of presentation  • sketches, layouts, dummies, etc.  • production of proofs 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- specialised design activities, see 74.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398673
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				18.14 
			</td>
		

			<td valign='top'>
				18.1 
			</td>
		

			<td valign='top'>
				Binding and related services 
			</td>
		

			<td valign='top'>
				1812 
			</td>
		

			<td valign='top'>
				This class includes:- trade binding, sample mounting and post press services in support of printing activities, e.g. trade binding and finishing of books, brochures, magazines, catalogues, etc., by folding, cutting and trimming, assembling, stitching, thread sewing, adhesive binding, cutting and cover laying, gluing, collating, basting, gold stamping; spiral binding and plastic wire binding- binding and finishing of printed paper or printed cardboard, by folding, stamping, drilling, punching, perforating, embossing, sticking, gluing, laminating- finishing services for CD-ROMs- mailing finishing services such as customisation, envelope preparation- other finishing activities such as die, sinking or stamping, Braille copying 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398674
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				18.2 
			</td>
		

			<td valign='top'>
				18 
			</td>
		

			<td valign='top'>
				Reproduction of recorded media 
			</td>
		

			<td valign='top'>
				182 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398675
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				18.20 
			</td>
		

			<td valign='top'>
				18.2 
			</td>
		

			<td valign='top'>
				Reproduction of recorded media 
			</td>
		

			<td valign='top'>
				1820 
			</td>
		

			<td valign='top'>
				This class includes:- reproduction from master copies of gramophone records, compact discs and tapes with music or other sound recordings- reproduction from master copies of records, compact discs and tapes with motion pictures and other video recordings- reproduction from master copies of software and data on discs and tapes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- reproduction of printed matter, see 18.11, 18.12- publishing of software, see 58.2- production and distribution of motion pictures, video tapes and movies on DVD or similar media, see 59.11, 59.12, 59.13- reproduction of motion picture film for theatrical distribution, see 59.12- production of master copies for records or audio material, see 59.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398676
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				19 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of coke and refined petroleum products 
			</td>
		

			<td valign='top'>
				19 
			</td>
		

			<td valign='top'>
				This division includes the transformation of crude petroleum and coal into usable products. The dominant process is petroleum refining, which involves the separation of crude petroleum into component products through such techniques as cracking and distillation. This division includes the manufacture of gases such as ethane, propane and butane as products of petroleum refineries. 
			</td>
		

			<td valign='top'>
				This division also includes the manufacture for own account of characteristic products (e.g. coke, butane, propane, petrol, kerosene, fuel oil etc.) as well as processing services (e.g. custom refining). 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				Not included is the manufacture of such gases in other units (20.14), manufacture of industrial gases (20.11), extraction of natural gas (methane, ethane, butane or propane) (06.20), and manufacture of fuel gas, other than petroleum gases (e.g. coal gas, water gas, producer gas, gasworks gas) (35.21).The manufacture of petrochemicals from refined petroleum is classified in division 20. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398677
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				19.1 
			</td>
		

			<td valign='top'>
				19 
			</td>
		

			<td valign='top'>
				Manufacture of coke oven products 
			</td>
		

			<td valign='top'>
				191 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398678
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				19.10 
			</td>
		

			<td valign='top'>
				19.1 
			</td>
		

			<td valign='top'>
				Manufacture of coke oven products 
			</td>
		

			<td valign='top'>
				1910 
			</td>
		

			<td valign='top'>
				This class includes:- operation of coke ovens- production of coke and semi-coke- production of pitch and pitch coke- production of coke oven gas- production of crude coal and lignite tars- agglomeration of coke 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of coal fuel briquettes, see 19.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398679
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				19.2 
			</td>
		

			<td valign='top'>
				19 
			</td>
		

			<td valign='top'>
				Manufacture of refined petroleum products 
			</td>
		

			<td valign='top'>
				192 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398680
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				19.20 
			</td>
		

			<td valign='top'>
				19.2 
			</td>
		

			<td valign='top'>
				Manufacture of refined petroleum products 
			</td>
		

			<td valign='top'>
				1920 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of liquid or gaseous fuels or other products from crude petroleum, bituminous minerals or their fractionation products. Petroleum refining involves one or more of the following activities: fractionation; straight distillation of crude oil; and cracking.This class includes:- production of motor fuel: gasoline, kerosene etc.- production of fuel: light, medium and heavy fuel oil, refinery gases such as ethane, propane, butane etc.- manufacture of oil-based lubricating oils or greases, including from waste oil- manufacture of products for the petrochemical industry and for the manufacture of road coverings- manufacture of various products: white spirit, vaseline, paraffin wax, petroleum jelly etc.- manufacture of petroleum briquettes- blending of biofuels, i.e. blending of alcohols with petroleum (e.g. gasohol) 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of peat briquettes- manufacture of hard-coal and lignite fuel briquettes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398681
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of chemicals and chemical products 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				This division includes the transformation of organic and inorganic raw materials by a chemical process and the formation of products. It distinguishes the production of basic chemicals that constitute the first industry group from the production of intermediate and end products produced by further processing of basic chemicals that make up the remaining industry classes. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398682
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				Manufacture of basic chemicals, fertilisers and nitrogen compounds, plastics and synthetic rubber in primary forms 
			</td>
		

			<td valign='top'>
				201 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of basic chemical products, fertilisers and associated nitrogen compounds, as well as plastics and synthetic rubber in primary forms. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398683
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.11 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of industrial gases 
			</td>
		

			<td valign='top'>
				2011 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of liquefied or compressed inorganic industrial or medical gases:  • elemental gases  • liquid or compressed air  • refrigerant gases  • mixed industrial gases  • inert gases such as carbon dioxide  • isolating gases 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Mixing of gases 
			</td>
		

			<td valign='top'>
				This class excludes:- extraction of methane, ethane, butane or propane, see 06.20- manufacture of fuel gases such as ethane, butane or propane in a petroleum refinery, see 19.20- manufacture of gaseous fuels from coal, waste etc., see 35.21 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398684
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.12 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of dyes and pigments 
			</td>
		

			<td valign='top'>
				2011 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of dyes and pigments from any source in basic form or as concentrate 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of products of a kind used as fluorescent brightening agents or as luminophores 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of prepared dyes and pigments, see 20.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398685
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.13 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of other inorganic basic chemicals 
			</td>
		

			<td valign='top'>
				2011 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of chemicals using basic processes. The output of these processes are usually separate chemical elements or separate chemically defined compounds.This class includes:- manufacture of chemical elements (except industrial gases and basic metals)- manufacture of inorganic acids except nitric acid- manufacture of alkalis, lyes and other inorganic bases except ammonia- manufacture of other inorganic compounds- roasting of iron pyrites- manufacture of distilled water 
			</td>
		

			<td valign='top'>
				This class also includes:- enrichment of uranium and thorium ores 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of industrial gases, see 20.11- manufacture of nitrogenous fertilisers and nitrogen compounds, see 20.15- manufacture of ammonia, see 20.15- manufacture of ammonium chloride, see 20.15- manufacture of nitrites and nitrates of potassium, see 20.15- manufacture of ammonium carbonates, see 20.15- manufacture of aromatic distilled water, see 20.53- manufacture of basic metals, see division 24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398686
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.14 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of other organic basic chemicals 
			</td>
		

			<td valign='top'>
				2011 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of chemicals using basic processes, such as thermal cracking and distillation. The output of these processes are usually separate chemical elements or separate chemically defined compounds.This class includes:- manufacture of basic organic chemicals:  • acyclic hydrocarbons, saturated and unsaturated  • cyclic hydrocarbons, saturated and unsaturated  • acyclic and cyclic alcohols  • mono- and polycarboxylic acids, including acetic acid  • other oxygen-function compounds, including aldehydes, ketones, quinones and dual or poly oxygen-function compounds  • synthetic glycerol  • nitrogen-function organic compounds, including amines  • fermentation of sugarcane, corn or similar to produce alcohol and esters  • other organic compounds, including wood distillation products (e.g. charcoal) etc.- manufacture of synthetic aromatic products- distillation of coal tar 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of plastics in primary forms, see 20.16- manufacture of synthetic rubber in primary forms, see 20.17- manufacture of crude glycerol, see 20.41- manufacture of natural essential oils, see 20.53- manufacture of salicylic and O-acetylsalicylic acids, see 21.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398687
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.15 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of fertilisers and nitrogen compounds 
			</td>
		

			<td valign='top'>
				2012 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of fertilisers:  • straight or complex nitrogenous, phosphatic or potassic fertilisers  • urea, crude natural phosphates and crude natural potassium salts- manufacture of associated nitrogen products:  • nitric and sulphonitric acids, ammonia, ammonium chloride, ammonium carbonate, nitrites and nitrates of potassium 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of potting soil with peat as main constituent- manufacture of potting soil mixtures of natural soil, sand, clays and minerals 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- mining of guano, see 08.91- manufacture of agrochemical products, such as pesticides, see 20.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398688
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.16 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of plastics in primary forms 
			</td>
		

			<td valign='top'>
				2013 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of resins, plastics materials and non-vulcanisable thermoplastic elastomers, the mixing and blending of resins on a custom basis, as well as the manufacture of non-customised synthetic resins.This class includes:- manufacture of plastics in primary forms:  • polymers, including those of ethylene, propylene, styrene, vinyl chloride, vinyl acetate and acrylics  • polyamides  • phenolic and epoxide resins and polyurethanes  • alkyd and polyester resins and polyethers  • silicones  • ion-exchangers based on polymers 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of cellulose and its chemical derivatives 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of artificial and synthetic fibres, filaments and yarn, see 20.60- shredding of plastic products, see 38.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398689
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.17 
			</td>
		

			<td valign='top'>
				20.1 
			</td>
		

			<td valign='top'>
				Manufacture of synthetic rubber in primary forms 
			</td>
		

			<td valign='top'>
				2013 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of synthetic rubber in primary forms:  • synthetic rubber  • factice- manufacture of mixtures of synthetic rubber and natural rubber or rubber-like gums (e.g. balata) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398690
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				20.2 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				Manufacture of pesticides and other agrochemical products 
			</td>
		

			<td valign='top'>
				202 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398691
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.20 
			</td>
		

			<td valign='top'>
				20.2 
			</td>
		

			<td valign='top'>
				Manufacture of pesticides and other agrochemical products 
			</td>
		

			<td valign='top'>
				2021 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of insecticides, rodenticides, fungicides, herbicides, acaricides, molluscicides, biocides- manufacture of anti-sprouting products, plant growth regulators- manufacture of disinfectants (for agricultural and other use)- manufacture of other agrochemical products n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of fertilisers and nitrogen compounds, see 20.15 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398692
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				20.3 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				Manufacture of paints, varnishes and similar coatings, printing ink and mastics 
			</td>
		

			<td valign='top'>
				202 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398693
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.30 
			</td>
		

			<td valign='top'>
				20.3 
			</td>
		

			<td valign='top'>
				Manufacture of paints, varnishes and similar coatings, printing ink and mastics 
			</td>
		

			<td valign='top'>
				2022 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of paints and varnishes, enamels or lacquers- manufacture of prepared pigments and dyes, opacifiers and colours- manufacture of vitrifiable enamels and glazes and engobes and similar preparations- manufacture of mastics- manufacture of caulking compounds and similar non-refractory filling or surfacing preparations- manufacture of organic composite solvents and thinners- manufacture of prepared paint or varnish removers- manufacture of printing ink 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of dyestuffs and pigments, see 20.12- manufacture of writing and drawing ink, see 20.59 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398694
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				20.4 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				Manufacture of soap and detergents, cleaning and polishing preparations, perfumes and toilet preparations 
			</td>
		

			<td valign='top'>
				202 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398695
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.41 
			</td>
		

			<td valign='top'>
				20.4 
			</td>
		

			<td valign='top'>
				Manufacture of soap and detergents, cleaning and polishing preparations 
			</td>
		

			<td valign='top'>
				2023 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of organic surface-active agents- manufacture of paper, wadding, felt etc. coated or covered with soap or detergent- manufacture of glycerol- manufacture of soap, except cosmetic soap- manufacture of surface-active preparations:  • washing powders in solid or liquid form and detergents  • dish-washing preparations  • textile softeners- manufacture of cleaning and polishing products:  • preparations for perfuming or deodorising rooms  • artificial waxes and prepared waxes  • polishes and creams for leather  • polishes and creams for wood  • polishes for coachwork, glass and metal  • scouring pastes and powders, including paper, wadding etc. coated or covered with these 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of separate, chemically defined compounds, see 20.13, 20.14- manufacture of glycerol, synthesised from petroleum products, see 20.14- manufacture of cosmetic soap, see 20.42 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398696
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.42 
			</td>
		

			<td valign='top'>
				20.4 
			</td>
		

			<td valign='top'>
				Manufacture of perfumes and toilet preparations 
			</td>
		

			<td valign='top'>
				2023 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of perfumes and toilet preparations:  • perfumes and toilet water  • beauty and make-up preparations  • sunburn prevention and suntan preparations  • manicure and pedicure preparations  • shampoos, hair lacquers, waving and straightening preparations  • dentifrices and preparations for oral hygiene, including denture fixative preparations  • shaving preparations, including pre-shave and aftershave preparations  • deodorants and bath salts  • depilatories- manufacture of cosmetic soap 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of skin care gel 
			</td>
		

			<td valign='top'>
				This class excludes:- extraction and refining of natural essential oils, see 20.53 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398697
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				20.5 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				Manufacture of other chemical products 
			</td>
		

			<td valign='top'>
				202 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of explosives and pyrotechnic products, glues, essential oils and chemical products n.e.c., e.g. photographic chemical material (including film and sensitised paper), composite diagnostic preparations etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398698
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.51 
			</td>
		

			<td valign='top'>
				20.5 
			</td>
		

			<td valign='top'>
				Manufacture of explosives 
			</td>
		

			<td valign='top'>
				2029 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of propellant powders- manufacture of explosives and pyrotechnic products, including percussion caps, detonators, signalling flares etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of matches 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398699
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.52 
			</td>
		

			<td valign='top'>
				20.5 
			</td>
		

			<td valign='top'>
				Manufacture of glues 
			</td>
		

			<td valign='top'>
				2029 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of glues and prepared adhesives, including rubber-based glues and adhesives 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of gelatines and its derivates, see 20.59 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398700
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.53 
			</td>
		

			<td valign='top'>
				20.5 
			</td>
		

			<td valign='top'>
				Manufacture of essential oils 
			</td>
		

			<td valign='top'>
				2029 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of extracts of natural aromatic products- manufacture of resinoids- manufacture of mixtures of odoriferous products for the manufacture of perfumes or food 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of synthetic aromatic products, see 20.14- manufacture of perfumes and toilet preparations, see 20.42 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398701
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.59 
			</td>
		

			<td valign='top'>
				20.5 
			</td>
		

			<td valign='top'>
				Manufacture of other chemical products n.e.c. 
			</td>
		

			<td valign='top'>
				2029 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of photographic plates, films, sensitised paper and other sensitised unexposed materials- manufacture of chemical preparations for photographic uses- manufacture of gelatine and its derivatives- manufacture of various chemical products:  • peptones, peptone derivatives, other protein substances and their derivatives n.e.c.  • chemically modified oils and fats  • materials used in the finishing of textiles and leather  • powders and pastes used in soldering, brazing or welding  • substances used to pickle metal  • prepared additives for cements  • activated carbon, lubricating oil additives, prepared rubber accelerators, catalysts and other chemical products for industrial use  • anti-knock preparations, antifreeze preparations  • liquids for hydraulic transmission  • composite diagnostic or laboratory reagents 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of writing and drawing ink 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of chemically defined products in bulk, see 20.13, 20.14- manufacture of distilled water, 20.13- manufacture of other organic basic chemicals, see 20.14- manufacture of printing ink, see 20.30- manufacture of asphalt-based adhesives, see 23.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398702
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				20.6 
			</td>
		

			<td valign='top'>
				20 
			</td>
		

			<td valign='top'>
				Manufacture of man-made fibres 
			</td>
		

			<td valign='top'>
				203 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398703
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				20.60 
			</td>
		

			<td valign='top'>
				20.6 
			</td>
		

			<td valign='top'>
				Manufacture of man-made fibres 
			</td>
		

			<td valign='top'>
				2030 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of synthetic or artificial filament tow- manufacture of synthetic or artificial staple fibres, not carded, combed or otherwise processed for spinning- manufacture of synthetic or artificial filament yarn, including high-tenacity yarn- manufacture of synthetic or artificial monofilament or strip 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- spinning of synthetic or artificial fibres, see 13.10- manufacture of yarns made of man-made staple, see 13.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398704
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				21 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of basic pharmaceutical products and pharmaceutical preparations 
			</td>
		

			<td valign='top'>
				21 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of basic pharmaceutical products and pharmaceutical preparations. 
			</td>
		

			<td valign='top'>
				This also includes the manufacture of medicinal chemical and botanical products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398705
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				21.1 
			</td>
		

			<td valign='top'>
				21 
			</td>
		

			<td valign='top'>
				Manufacture of basic pharmaceutical products 
			</td>
		

			<td valign='top'>
				210 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398706
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				21.10 
			</td>
		

			<td valign='top'>
				21.1 
			</td>
		

			<td valign='top'>
				Manufacture of basic pharmaceutical products 
			</td>
		

			<td valign='top'>
				2100 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of medicinal active substances to be used for their pharmacological properties in the manufacture of medicaments: antibiotics, basic vitamins, salicylic and O-acetylsalicylic acids etc.- processing of blood 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of chemically pure sugars- processing of glands and manufacture of extracts of glands etc. 
			</td>
		

			<td valign='top'>
				- Manufacture of probiotics 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398707
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				21.2 
			</td>
		

			<td valign='top'>
				21 
			</td>
		

			<td valign='top'>
				Manufacture of pharmaceutical preparations 
			</td>
		

			<td valign='top'>
				210 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398708
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				21.20 
			</td>
		

			<td valign='top'>
				21.2 
			</td>
		

			<td valign='top'>
				Manufacture of pharmaceutical preparations 
			</td>
		

			<td valign='top'>
				2100 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of medicaments:  • antisera and other blood fractions  • vaccines  • diverse medicaments, including homeopathic preparations- manufacture of chemical contraceptive products for external use and hormonal contraceptive medicaments- manufacture of medical diagnostic preparations, including pregnancy tests- manufacture of radioactive in-vivo diagnostic substances- manufacture of biotech pharmaceuticals 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of medical impregnated wadding, gauze, bandages, dressings etc.- preparation of botanical products (grinding, grading, milling) for pharmaceutical use 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of herb infusions (mint, vervain, chamomile etc.), see 10.83- manufacture of dental fillings and dental cement, see 32.50- manufacture of bone reconstruction cements, see 32.50- manufacture of surgical drapes, see 32.50- wholesale of pharmaceuticals, see 46.46- retail sale of pharmaceuticals, see 47.73- research and development for pharmaceuticals and biotech pharmaceuticals, see 72.1- packaging of pharmaceuticals, see 82.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398709
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				22 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of rubber and plastic products 
			</td>
		

			<td valign='top'>
				22 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of rubber and plastics products. This division is characterised by the raw materials used in the manufacturing process. However, this does not imply that the manufacture of all products made of these materials is classified here. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398710
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				22.1 
			</td>
		

			<td valign='top'>
				22 
			</td>
		

			<td valign='top'>
				Manufacture of rubber products 
			</td>
		

			<td valign='top'>
				221 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of rubber products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398711
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				22.11 
			</td>
		

			<td valign='top'>
				22.1 
			</td>
		

			<td valign='top'>
				Manufacture of rubber tyres and tubes; retreading and rebuilding of rubber tyres 
			</td>
		

			<td valign='top'>
				2211 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of rubber tyres for vehicles, equipment, mobile machinery, aircraft, toy, furniture and other uses:  • pneumatic tyres  • solid or cushion tyres- manufacture of inner tubes for tyres- manufacture of interchangeable tyre treads, tyre flaps, &quot;camelback&quot; strips for retreading tyres etc.- tyre rebuilding and retreading 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of tube repair materials, see 22.19- tyre and tube repair, fitting or replacement, see 45.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398712
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				22.19 
			</td>
		

			<td valign='top'>
				22.1 
			</td>
		

			<td valign='top'>
				Manufacture of other rubber products 
			</td>
		

			<td valign='top'>
				2219 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of other products of natural or synthetic rubber, unvulcanised, vulcanised or hardened:  • rubber plates, sheets, strip, rods, profile shapes  • tubes, pipes and hoses  • rubber conveyor or transmission belts or belting  • rubber hygienic articles: sheath contraceptives, teats, hot water bottles etc.  • rubber articles of apparel (if only sealed together, not sewn)  • rubber sole and other rubber parts of footwear  • rubber thread and rope  • rubberised yarn and fabrics  • rubber rings, fittings and seals  • rubber roller coverings  • inflatable rubber mattresses  • inflatable balloons- manufacture of rubber brushes- manufacture of hard rubber pipe stems- manufacture of hard rubber combs, hair pins, hair rollers, and similar 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of rubber repair materials- manufacture of textile fabric impregnated, coated, covered or laminated with rubber, where rubber is the chief constituent- manufacture of rubber waterbed mattresses- manufacture of rubber bathing caps and aprons- manufacture of rubber wet suits and diving suits- manufacture of rubber sex articles 
			</td>
		

			<td valign='top'>
				- Manufacture of apparel made of latex 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of tyre cord fabrics, see 13.96- manufacture of apparel of elastic fabrics, see 14.14, 14.19- manufacture of rubber footwear, see 15.20- manufacture of glues and adhesives based on rubber, see 20.52- manufacture of &quot;camelback&quot; strips, see 22.11- manufacture of inflatable rafts and boats, see 30.11, 30.12- manufacture of mattresses of uncovered cellular rubber, see 31.03- manufacture of rubber sports requisites, except apparel, see 32.30- manufacture of rubber games and toys (including children's wading pools, inflatable children rubber boats, inflatable rubber animals, balls and the like), see 32.40- reclaiming of rubber, see 38.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398713
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				22.2 
			</td>
		

			<td valign='top'>
				22 
			</td>
		

			<td valign='top'>
				Manufacture of plastic products 
			</td>
		

			<td valign='top'>
				222 
			</td>
		

			<td valign='top'>
				This group comprises processing new or spent (i.e., recycled) plastics resins into intermediate or final products, using such processes as compression moulding; extrusion moulding; injection moulding; blow moulding; and casting. For most of these, the production process is such that a wide variety of products can be made. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398714
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				22.21 
			</td>
		

			<td valign='top'>
				22.2 
			</td>
		

			<td valign='top'>
				Manufacture of plastic plates, sheets, tubes and profiles 
			</td>
		

			<td valign='top'>
				2220 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of semi-manufactures of plastic products:  • plastic plates, sheets, blocks, film, foil, strip etc. (whether self-adhesive or not)- manufacture of finished plastic products:  • plastic tubes, pipes and hoses; hose and pipe fittings  • cellophane film or sheet 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of plastics in primary forms, see 20.16- manufacture of articles of synthetic or natural rubber, see 22.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398715
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				22.22 
			</td>
		

			<td valign='top'>
				22.2 
			</td>
		

			<td valign='top'>
				Manufacture of plastic packing goods 
			</td>
		

			<td valign='top'>
				2220 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of plastic articles for the packing of goods:  • plastic bags, sacks, containers, boxes, cases, carboys, bottles etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of luggage and handbags of plastic, see 15.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398716
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				22.23 
			</td>
		

			<td valign='top'>
				22.2 
			</td>
		

			<td valign='top'>
				Manufacture of builders’ ware of plastic 
			</td>
		

			<td valign='top'>
				2220 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of builders' plastics ware:  • plastic doors, windows, frames, shutters, blinds, skirting boards  • tanks, reservoirs  • plastic floor, wall or ceiling coverings in rolls or in the form of tiles etc.  • plastic sanitary ware like plastic baths, shower baths, washbasins, lavatory pans, flushing cisterns etc.- manufacture of resilient floor coverings, such as vinyl, linoleum etc.- manufacture of artificial stone (e.g. cultured marble) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398717
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				22.29 
			</td>
		

			<td valign='top'>
				22.2 
			</td>
		

			<td valign='top'>
				Manufacture of other plastic products 
			</td>
		

			<td valign='top'>
				2220 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of plastic tableware, kitchenware and toilet articles- manufacture of diverse plastic products:  • plastic headgear, insulating fittings, parts of lighting fittings, office or school supplies, articles of apparel (if only sealed together, not sewn), fittings for furniture, statuettes, transmission and conveyer belts, self-adhesive tapes of plastic, plastic shoe lasts, plastic cigar and cigarette holders, combs, plastics hair curlers, plastics novelties, etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacturing of carbon fibre masts- Welding of plastic tanks and pipes 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of plastic luggage, see 15.12- manufacture of plastic footwear, see 15.20- manufacture of plastic furniture, see 31.01, 31.02, 31.09- manufacture of mattresses of uncovered cellular plastic, see 31.03- manufacture of plastic sports requisites, see 32.30- manufacture of plastic games and toys, see 32.40- manufacture of plastic medical and dental appliances, see 32.50- manufacture of plastic ophthalmic goods, see 32.50- manufacture of plastics hard hats and other personal safety equipment of plastics, see 32.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398718
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of other non-metallic mineral products 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				This division includes manufacturing activities related to a single substance of mineral origin. This division includes the manufacture of glass and glass products (e.g. flat glass, hollow glass, fibres, technical glassware etc.), ceramic products, tiles and baked clay products, and cement and plaster, from raw materials to finished articles. 
			</td>
		

			<td valign='top'>
				The manufacture of shaped and finished stone and other mineral products is also included in this division. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398719
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.1 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of glass and glass products 
			</td>
		

			<td valign='top'>
				231 
			</td>
		

			<td valign='top'>
				This group includes glass in all its forms, made by any process, and articles of glass. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398720
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.11 
			</td>
		

			<td valign='top'>
				23.1 
			</td>
		

			<td valign='top'>
				Manufacture of flat glass 
			</td>
		

			<td valign='top'>
				2310 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of flat glass, including wired, coloured or tinted flat glass 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398721
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.12 
			</td>
		

			<td valign='top'>
				23.1 
			</td>
		

			<td valign='top'>
				Shaping and processing of flat glass 
			</td>
		

			<td valign='top'>
				2310 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of toughened or laminated flat glass- manufacture of glass mirrors- manufacture of multiple-walled insulating units of glass 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398722
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.13 
			</td>
		

			<td valign='top'>
				23.1 
			</td>
		

			<td valign='top'>
				Manufacture of hollow glass 
			</td>
		

			<td valign='top'>
				2310 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of bottles and other containers of glass or crystal- manufacture of drinking glasses and other domestic glass or crystal articles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of glass toys, see 32.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398723
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.14 
			</td>
		

			<td valign='top'>
				23.1 
			</td>
		

			<td valign='top'>
				Manufacture of glass fibres 
			</td>
		

			<td valign='top'>
				2310 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of glass fibres, including glass wool and non-woven products thereof 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of woven fabrics of glass yarn, see 13.20- manufacture of fibre optic cable for data transmission or live transmission of images, see 27.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398724
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.19 
			</td>
		

			<td valign='top'>
				23.1 
			</td>
		

			<td valign='top'>
				Manufacture and processing of other glass, including technical glassware 
			</td>
		

			<td valign='top'>
				2310 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of laboratory, hygienic or pharmaceutical glassware- manufacture of clock or watch glasses, optical glass and optical elements not optically worked- manufacture of glassware used in imitation jewellery- manufacture of glass insulators and glass insulating fittings- manufacture of glass envelopes for lamps- manufacture of glass figurines- manufacture of glass paving blocks- manufacture of glass in rods or tubes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of optical elements optically worked, see 26.70- manufacture of syringes and other medical laboratory equipment, see 32.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398725
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.2 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of refractory products 
			</td>
		

			<td valign='top'>
				239 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398726
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.20 
			</td>
		

			<td valign='top'>
				23.2 
			</td>
		

			<td valign='top'>
				Manufacture of refractory products 
			</td>
		

			<td valign='top'>
				2391 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of intermediate products from mined or quarried non-metallic minerals, such as sand, gravel, stone or clay.This class includes:- manufacture of refractory mortars, concretes etc.- manufacture of refractory ceramic goods:  • heat-insulating ceramic goods of siliceous fossil meals  • refractory bricks, blocks and tiles etc.  • retorts, crucibles, muffles, nozzles, tubes, pipes etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of refractory articles containing magnesite, dolomite or chromite 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398727
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.3 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of clay building materials 
			</td>
		

			<td valign='top'>
				239 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398728
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.31 
			</td>
		

			<td valign='top'>
				23.3 
			</td>
		

			<td valign='top'>
				Manufacture of ceramic tiles and flags 
			</td>
		

			<td valign='top'>
				2392 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of non-refractory ceramic hearth or wall tiles, mosaic cubes etc.- manufacture of non-refractory ceramic flags and paving 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of artificial stone (e.g. cultured marble), see 22.23- manufacture of refractory ceramic products, see 23.20- manufacture of ceramic bricks and roofing tiles, see 23.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398729
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.32 
			</td>
		

			<td valign='top'>
				23.3 
			</td>
		

			<td valign='top'>
				Manufacture of bricks, tiles and construction products, in baked clay 
			</td>
		

			<td valign='top'>
				2392 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of structural non-refractory clay building materials:  • manufacture of ceramic bricks, roofing tiles, chimney pots, pipes, conduits etc.- manufacture of flooring blocks in baked clay 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of refractory ceramic products, see 23.20- manufacture of non-structural non-refractory ceramic products, see 23.4 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398730
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.4 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of other porcelain and ceramic products 
			</td>
		

			<td valign='top'>
				239 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of final products from mined or quarried non-metallic minerals, such as sand, gravel, stone or clay. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398731
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.41 
			</td>
		

			<td valign='top'>
				23.4 
			</td>
		

			<td valign='top'>
				Manufacture of ceramic household and ornamental articles 
			</td>
		

			<td valign='top'>
				2393 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ceramic tableware and other domestic or toilet articles- manufacture of statuettes and other ornamental ceramic articles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of imitation jewellery, see 32.13- manufacture of ceramic toys, see 32.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398732
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.42 
			</td>
		

			<td valign='top'>
				23.4 
			</td>
		

			<td valign='top'>
				Manufacture of ceramic sanitary fixtures 
			</td>
		

			<td valign='top'>
				2393 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ceramic sanitary fixtures, e.g. sinks, baths, bidets, water closet pans etc.- manufacture of other ceramic fixtures 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of refractory ceramic goods, see 23.20- manufacture of ceramic building materials, see 23.3 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398733
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.43 
			</td>
		

			<td valign='top'>
				23.4 
			</td>
		

			<td valign='top'>
				Manufacture of ceramic insulators and insulating fittings 
			</td>
		

			<td valign='top'>
				2393 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of electrical insulators and insulating fittings of ceramics 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of refractory ceramic goods, see 23.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398734
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.44 
			</td>
		

			<td valign='top'>
				23.4 
			</td>
		

			<td valign='top'>
				Manufacture of other technical ceramic products 
			</td>
		

			<td valign='top'>
				2393 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ceramic and ferrite magnets- manufacture of ceramic laboratory, chemical and industrial products 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of artificial stone (e.g. cultured marble), see 22.23- manufacture of refractory ceramic goods, see 23.20- manufacture of ceramic building materials, see 23.3 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398735
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.49 
			</td>
		

			<td valign='top'>
				23.4 
			</td>
		

			<td valign='top'>
				Manufacture of other ceramic products 
			</td>
		

			<td valign='top'>
				2393 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ceramic pots, jars and similar articles of a kind used for conveyance or packing of goods- manufacture of ceramic products n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of ceramic sanitary fixtures, see 23.42- manufacture of artificial teeth, see 32.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398736
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.5 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of cement, lime and plaster 
			</td>
		

			<td valign='top'>
				239 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398737
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.51 
			</td>
		

			<td valign='top'>
				23.5 
			</td>
		

			<td valign='top'>
				Manufacture of cement 
			</td>
		

			<td valign='top'>
				2394 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of clinkers and hydraulic cements, including Portland, aluminous cement, slag cement and superphosphate cements 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of refractory mortars, concrete etc., see 23.20- manufacture of ready-mixed and dry-mix concrete and mortars, see 23.63, 23.64- manufacture of articles of cement, see 23.69- manufacture of cements used in dentistry, see 32.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398738
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.52 
			</td>
		

			<td valign='top'>
				23.5 
			</td>
		

			<td valign='top'>
				Manufacture of lime and plaster 
			</td>
		

			<td valign='top'>
				2394 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of quicklime, slaked lime and hydraulic lime- manufacture of plasters of calcined gypsum or calcined sulphate 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of calcined dolomite 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of articles of plaster, see 23.62, 23.69 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398739
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of articles of concrete, cement and plaster 
			</td>
		

			<td valign='top'>
				239 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398740
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.61 
			</td>
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				Manufacture of concrete products for construction purposes 
			</td>
		

			<td valign='top'>
				2395 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of precast concrete, cement or artificial stone articles for use in construction:  • tiles, flagstones, bricks, boards, sheets, panels, pipes, posts etc.- manufacture of prefabricated structural components for building or civil engineering of cement, concrete or artificial stone 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398741
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.62 
			</td>
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				Manufacture of plaster products for construction purposes 
			</td>
		

			<td valign='top'>
				2395 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of plaster articles for use in construction:  • boards, sheets, panels etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398742
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.63 
			</td>
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				Manufacture of ready-mixed concrete 
			</td>
		

			<td valign='top'>
				2395 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ready-mix and dry-mix concrete and mortars 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of refractory cements, see 23.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398743
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.64 
			</td>
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				Manufacture of mortars 
			</td>
		

			<td valign='top'>
				2395 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of powdered mortars 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of refractory mortars, see 23.20- manufacture of dry-mixed concrete and mortars, see 23.63 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398744
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.65 
			</td>
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				Manufacture of fibre cement 
			</td>
		

			<td valign='top'>
				2395 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of building materials of vegetable substances (wood wool, straw, reeds, rushes) agglomerated with cement, plaster or other mineral binder- manufacture of articles of asbestos-cement or cellulose fibre-cement or the like:  • corrugated sheets, other sheets, panels, tiles, tubes, pipes, reservoirs, troughs, basins, sinks, jars, furniture, window frames etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398745
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.69 
			</td>
		

			<td valign='top'>
				23.6 
			</td>
		

			<td valign='top'>
				Manufacture of other articles of concrete, plaster and cement 
			</td>
		

			<td valign='top'>
				2395 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of other articles of concrete, plaster, cement or artificial stone:  • statuary, furniture, bas- and haut-reliefs, vases, flowerpots etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398746
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.7 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Cutting, shaping and finishing of stone 
			</td>
		

			<td valign='top'>
				239 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398747
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.70 
			</td>
		

			<td valign='top'>
				23.7 
			</td>
		

			<td valign='top'>
				Cutting, shaping and finishing of stone 
			</td>
		

			<td valign='top'>
				2396 
			</td>
		

			<td valign='top'>
				This class includes:- cutting, shaping and finishing of stone for use in construction, in cemeteries, on roads, as roofing etc.- manufacture of stone furniture 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities carried out by operators of quarries, e.g. production of rough cut stone, see 08.11- production of millstones, abrasive stones and similar products, see 23.9 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398748
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				23.9 
			</td>
		

			<td valign='top'>
				23 
			</td>
		

			<td valign='top'>
				Manufacture of abrasive products and non-metallic mineral products n.e.c. 
			</td>
		

			<td valign='top'>
				239 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of other non-metallic mineral products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398749
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.91 
			</td>
		

			<td valign='top'>
				23.9 
			</td>
		

			<td valign='top'>
				Production of abrasive products 
			</td>
		

			<td valign='top'>
				2399 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of millstones, sharpening or polishing stones and natural or artificial abrasive products on a support, including abrasive products on a soft base (e.g. sandpaper) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398750
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				23.99 
			</td>
		

			<td valign='top'>
				23.9 
			</td>
		

			<td valign='top'>
				Manufacture of other non-metallic mineral products n.e.c. 
			</td>
		

			<td valign='top'>
				2399 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of friction material and unmounted articles thereof with a base of mineral substances or of cellulose- manufacture of mineral insulating materials:  • slag wool, rock wool and similar mineral wools; exfoliated vermiculite, expanded clays and similar heat-insulating, sound-insulating or sound-absorbing materials- manufacture of articles of diverse mineral substances:  • worked mica and articles of mica, of peat, of graphite (other than electrical articles) etc. - manufacture of articles of asphalt or similar material, e.g. asphalt-based adhesives, coal tar pitch etc.- manufacture of carbon and graphite fibres and products (except electrodes and electrical applications)- manufacture of artificial corundum 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Calcination of kaolin- Drying and grinding of clay into clay powder, fit for the production of ceramics 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of glass wool and non-woven glass wool products, see 23.14- manufacture of graphite electrodes, see 27.90- manufacture of carbon or graphite gaskets, see 28.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398751
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of basic metals 
			</td>
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				This division includes the activities of smelting and/or refining ferrous and non-ferrous metals from ore, pig or scrap, using electrometallurgic and other process metallurgic techniques. 
			</td>
		

			<td valign='top'>
				This division also includes the manufacture of metal alloys and super-alloys by introducing other chemical elements to pure metals. The output of smelting and refining, usually in ingot form, is used in rolling, drawing and extruding operations to make products such as plate, sheet, strip, bars, rods, wire or tubes, pipes and hollow profiles, and in molten form to make castings and other basic metal products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398752
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				24.1 
			</td>
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				Manufacture of basic iron and steel and of ferro-alloys 
			</td>
		

			<td valign='top'>
				241 
			</td>
		

			<td valign='top'>
				This group includes activities such as direct reduction of iron ore, production of pig iron in molten or solid form, conversion of pig iron into steel, manufacture of ferroalloys and manufacture of steel products. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398753
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.10 
			</td>
		

			<td valign='top'>
				24.1 
			</td>
		

			<td valign='top'>
				Manufacture of basic iron and steel and of ferro-alloys 
			</td>
		

			<td valign='top'>
				2410 
			</td>
		

			<td valign='top'>
				This class includes:- operation of blast furnaces, steel converters, rolling and finishing mills- production of pig iron and spiegeleisen in pigs, blocks or other primary forms- production of ferro-alloys- production of ferrous products by direct reduction of iron and other spongy ferrous products- production of iron of exceptional purity by electrolysis or other chemical processes- remelting of scrap ingots of iron or steel- production of granular iron and iron powder- production of steel in ingots or other primary forms- production of semi-finished products of steel- manufacture of hot-rolled and cold-rolled flat-rolled products of steel- manufacture of hot-rolled bars and rods of steel- manufacture of hot-rolled open sections of steel- manufacture of sheet piling of steel and welded open sections of steel- manufacture of railway track materials (unassembled rails) of steel 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- cold drawing of bars, see 24.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398754
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				24.2 
			</td>
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				Manufacture of tubes, pipes, hollow profiles and related fittings, of steel 
			</td>
		

			<td valign='top'>
				241 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398755
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.20 
			</td>
		

			<td valign='top'>
				24.2 
			</td>
		

			<td valign='top'>
				Manufacture of tubes, pipes, hollow profiles and related fittings, of steel 
			</td>
		

			<td valign='top'>
				2410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of seamless tubes and pipes of circular or non-circular cross section and of blanks of circular cross section, for further processing, by hot rolling, hot extrusion or by other hot processes of an intermediate product which can be a bar or a billet obtained by hot rolling or continuous casting- manufacture of precision and non-precision seamless tubes and pipes from hot rolled or hot extruded blanks by further processing, by cold-drawing or cold-rolling of tubes and pipes of circular cross section and by cold drawing only for tubes and pipes of non circular cross section and hollow profiles- manufacture of welded tubes and pipes of an external diameter exceeding 406,4 mm, cold formed from hot rolled flat products and longitudinally or spirally welded- manufacture of welded tubes and pipes of an external diameter of 406,4 mm or less of circular cross section by continuous cold or hot forming of hot or cold rolled flat products and longitudinally or spirally welded and of non-circular cross section by hot or cold forming into shape from hot or cold rolled strip longitudinally welded- manufacture of welded precision tubes and pipes of an external diameter of 406,4 mm or less by hot or cold forming of hot or cold rolled strip and longitudinally welded delivered as welded or further processed, by cold drawing or cold rolling or cold formed into shape for tube and pipe of non-circular cross section- manufacture of flat flanges and flanges with forged collars by processing of hot rolled flat products of steel- manufacture of butt-welding fittings, such as elbows and reductions, by forging of hot rolled seamless tubes of steel- threaded and other tube or pipe fittings of steel 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes- manufacture of seamless tubes and pipes of steel by centrifugally casting, see 24.52 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398756
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				24.3 
			</td>
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				Manufacture of other products of first processing of steel 
			</td>
		

			<td valign='top'>
				241 
			</td>
		

			<td valign='top'>
				This group includes manufacturing other products by cold processing of steel. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398757
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.31 
			</td>
		

			<td valign='top'>
				24.3 
			</td>
		

			<td valign='top'>
				Cold drawing of bars 
			</td>
		

			<td valign='top'>
				2410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of steel bars and solid sections of steel by cold drawing, grinding or turning 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- drawing of wire, see 24.34 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398758
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.32 
			</td>
		

			<td valign='top'>
				24.3 
			</td>
		

			<td valign='top'>
				Cold rolling of narrow strip 
			</td>
		

			<td valign='top'>
				2410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of coated or uncoated flat rolled steel products in coils or in straight lengths of a width less than 600 mm by cold re-rolling of hot-rolled flat products or of steel rod 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398759
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.33 
			</td>
		

			<td valign='top'>
				24.3 
			</td>
		

			<td valign='top'>
				Cold forming or folding 
			</td>
		

			<td valign='top'>
				2410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of open sections by progressive cold forming on a roll mill or folding on a press of flat-rolled products of steel- manufacture of cold-formed or cold-folded, ribbed sheets and sandwich panels 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398760
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.34 
			</td>
		

			<td valign='top'>
				24.3 
			</td>
		

			<td valign='top'>
				Cold drawing of wire 
			</td>
		

			<td valign='top'>
				2410 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of drawn steel wire, by cold drawing of steel wire rod 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- drawing of bars and solid sections of steel, see 24.31- manufacture of derived wire products, see 25.93 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398761
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				Manufacture of basic precious and other non-ferrous metals 
			</td>
		

			<td valign='top'>
				242 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398762
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.41 
			</td>
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				Precious metals production 
			</td>
		

			<td valign='top'>
				2420 
			</td>
		

			<td valign='top'>
				This class includes:- production of basic precious metals:  • production and refining of unwrought or wrought precious metals: gold, silver, platinum etc. from ore and scrap- production of precious metal alloys- production of precious metal semi-products- production of silver rolled onto base metals- production of gold rolled onto base metals or silver- production of platinum and platinum group metals rolled onto gold, silver or base metals 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of wire of these metals by drawing- manufacture of precious metal foil laminates 
			</td>
		

			<td valign='top'>
				- Extraction of silver from waste chemicals, by electrolytic refining- Melting and casting of jewellery into slabs 
			</td>
		

			<td valign='top'>
				This class excludes:- casting of non-ferrous metals, see 24.53, 24.54- manufacture of precious metal jewellery, see 32.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398763
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.42 
			</td>
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				Aluminium production 
			</td>
		

			<td valign='top'>
				2420 
			</td>
		

			<td valign='top'>
				This class includes:- production of aluminium from alumina- production of aluminium from electrolytic refining of aluminium waste and scrap- production of aluminium alloys- semi-manufacture of aluminium 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of wire of these metals by drawing- production of aluminium oxide (alumina)- production of aluminium wrapping foil- manufacture of aluminium foil laminates made from aluminium foil as primary component 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- casting of non-ferrous metals, see 24.53, 24.54 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398764
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.43 
			</td>
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				Lead, zinc and tin production 
			</td>
		

			<td valign='top'>
				2420 
			</td>
		

			<td valign='top'>
				This class includes:- production of lead, zinc and tin from ores- production of lead, zinc and tin from electrolytic refining of lead, zinc and tin waste and scrap- production of lead, zinc and tin alloys- semi-manufacture of lead, zinc and tin 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of wire of these metals by drawing- production of tin foil 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- casting of non-ferrous metals, see 24.53, 24.54 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398765
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.44 
			</td>
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				Copper production 
			</td>
		

			<td valign='top'>
				2420 
			</td>
		

			<td valign='top'>
				This class includes:- production of copper from ores- production of copper from electrolytic refining of copper waste and scrap- production of copper alloys- manufacture of fuse wire or strip- semi-manufacture of copper 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of wire of these metals by drawing 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- casting of non-ferrous metals, see 24.53, 24.54 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398766
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.45 
			</td>
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				Other non-ferrous metal production 
			</td>
		

			<td valign='top'>
				2420 
			</td>
		

			<td valign='top'>
				This class includes:- production of chrome, manganese, nickel etc. from ores or oxides- production of chrome, manganese, nickel etc. from electrolytic and aluminothermic refining of chrome, manganese, nickel etc., waste and scrap- production of alloys of chrome, manganese, nickel etc. - semi-manufacture of chrome, manganese, nickel etc.- production of mattes of nickel 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of wire of these metals by drawing 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- casting of non-ferrous metals, see 24.53, 24.54 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398767
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.46 
			</td>
		

			<td valign='top'>
				24.4 
			</td>
		

			<td valign='top'>
				Processing of nuclear fuel 
			</td>
		

			<td valign='top'>
				2420 
			</td>
		

			<td valign='top'>
				This class includes:- production of uranium metal from pitchblende or other ores- smelting and refining of uranium 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398768
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				24.5 
			</td>
		

			<td valign='top'>
				24 
			</td>
		

			<td valign='top'>
				Casting of metals 
			</td>
		

			<td valign='top'>
				243 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of semi-finished products and various castings by a casting process. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- manufacture of finished cast products such as:  • boilers and radiators, see 25.21  • cast household items, see 25.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398769
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.51 
			</td>
		

			<td valign='top'>
				24.5 
			</td>
		

			<td valign='top'>
				Casting of iron 
			</td>
		

			<td valign='top'>
				2431 
			</td>
		

			<td valign='top'>
				This class includes activities of iron foundries.This class includes:- casting of semi-finished iron products- casting of grey iron castings- casting of spheroidal graphite iron castings- casting of malleable cast-iron products- manufacture of tubes, pipes and hollow profiles and of tube or pipe fittings of cast-iron 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398770
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.52 
			</td>
		

			<td valign='top'>
				24.5 
			</td>
		

			<td valign='top'>
				Casting of steel 
			</td>
		

			<td valign='top'>
				2431 
			</td>
		

			<td valign='top'>
				This class includes activities of steel foundries.This class includes:- casting of semi-finished steel products- casting of steel castings- manufacture of seamless tubes and pipes of steel by centrifugal casting- manufacture of tube or pipe fittings of cast-steel 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398771
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.53 
			</td>
		

			<td valign='top'>
				24.5 
			</td>
		

			<td valign='top'>
				Casting of light metals 
			</td>
		

			<td valign='top'>
				2432 
			</td>
		

			<td valign='top'>
				This class includes:- casting of semi-finished products of aluminium, magnesium, titanium, zinc etc.- casting of light metal castings 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398772
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				24.54 
			</td>
		

			<td valign='top'>
				24.5 
			</td>
		

			<td valign='top'>
				Casting of other non-ferrous metals 
			</td>
		

			<td valign='top'>
				2432 
			</td>
		

			<td valign='top'>
				This class includes:- casting of heavy metal castings- casting of precious metal castings- die-casting of non-ferrous metal castings 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398773
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of fabricated metal products, except machinery and equipment 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of &quot;pure&quot; metal products (such as parts, containers and structures), usually with a static, immovable function, as opposed to the following divisions 26-30, which cover the manufacture of combinations or assemblies of such metal products (sometimes with other materials) into more complex units that, unless they are purely electrical, electronic or optical, work with moving parts. 
			</td>
		

			<td valign='top'>
				The manufacture of weapons and ammunition is also included in this division. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes:- specialised repair and maintenance activities, see 33.1- specialised installation of manufactured goods produced in this division in buildings, such as central heating boilers, see 43.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398774
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.1 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Manufacture of structural metal products 
			</td>
		

			<td valign='top'>
				251 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of structural metal products (such as metal frameworks or parts for construction). 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398775
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.11 
			</td>
		

			<td valign='top'>
				25.1 
			</td>
		

			<td valign='top'>
				Manufacture of metal structures and parts of structures 
			</td>
		

			<td valign='top'>
				2511 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of metal frameworks or skeletons for construction and parts thereof (towers, masts, trusses, bridges etc.)- manufacture of industrial frameworks in metal (frameworks for blast furnaces, lifting and handling equipment etc.)- manufacture of prefabricated buildings mainly of metal:  • site huts, modular exhibition elements etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of parts for marine or power boilers, see 25.30- manufacture of assembled railway track fixtures, see 25.99- manufacture of sections of ships, see 30.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398776
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.12 
			</td>
		

			<td valign='top'>
				25.1 
			</td>
		

			<td valign='top'>
				Manufacture of doors and windows of metal 
			</td>
		

			<td valign='top'>
				2511 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of metal doors, windows and their frames, shutters and gates- metal room partitions for floor attachment 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398777
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.2 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Manufacture of tanks, reservoirs and containers of metal 
			</td>
		

			<td valign='top'>
				251 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of tanks, central heating radiators and boilers. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398778
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.21 
			</td>
		

			<td valign='top'>
				25.2 
			</td>
		

			<td valign='top'>
				Manufacture of central heating radiators and boilers 
			</td>
		

			<td valign='top'>
				2512 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of electrical ovens and water heaters, see 27.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398779
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.29 
			</td>
		

			<td valign='top'>
				25.2 
			</td>
		

			<td valign='top'>
				Manufacture of other tanks, reservoirs and containers of metal 
			</td>
		

			<td valign='top'>
				2512 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of reservoirs, tanks and similar containers of metal, of types normally installed as fixtures for storage or manufacturing use- manufacture of metal containers for compressed or liquefied gas 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of metal casks, drums, cans, pails, boxes etc. of a kind normally used for carrying and packing of goods, of a capacity not exceeding 300 litres, see 25.91, 25.92- manufacture of transport containers, see 29.20- manufacture of tanks (armoured military vehicles), see 30.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398780
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.3 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Manufacture of steam generators, except central heating hot water boilers 
			</td>
		

			<td valign='top'>
				251 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of steam generators. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398781
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.30 
			</td>
		

			<td valign='top'>
				25.3 
			</td>
		

			<td valign='top'>
				Manufacture of steam generators, except central heating hot water boilers 
			</td>
		

			<td valign='top'>
				2513 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of steam or other vapour generators- manufacture of auxiliary plant for use with steam generators:  • condensers, economisers, superheaters, steam collectors and accumulators- manufacture of nuclear reactors, except isotope separators- manufacture of parts for marine or power boilers 
			</td>
		

			<td valign='top'>
				This class also includes:- pipe system construction comprising further processing of tubes generally to make pressure pipes or pipe systems together with the associated design and construction work 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of central heating hot-water boilers and radiators, see 25.21- manufacture of boiler-turbine sets, see 28.11- manufacture of isotope separators, see 28.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398782
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.4 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Manufacture of weapons and ammunition 
			</td>
		

			<td valign='top'>
				252 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398783
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.40 
			</td>
		

			<td valign='top'>
				25.4 
			</td>
		

			<td valign='top'>
				Manufacture of weapons and ammunition 
			</td>
		

			<td valign='top'>
				2520 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of heavy weapons (artillery, mobile guns, rocket launchers, torpedo tubes, heavy machine guns)- manufacture of small arms (revolvers, shotguns, light machine guns)- manufacture of air or gas guns and pistols- manufacture of war ammunition 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of hunting, sporting or protective firearms and ammunition- manufacture of explosive devices such as bombs, mines and torpedoes 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of percussion caps, detonators or signalling flares, see 20.51- manufacture of cutlasses, swords, bayonets etc., see 25.71- manufacture of armoured vehicles for the transport of banknotes or valuables, see 29.10- manufacture of space vehicles, see 30.30- manufacture of tanks and other fighting vehicles, see 30.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398784
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.5 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Forging, pressing, stamping and roll-forming of metal; powder metallurgy 
			</td>
		

			<td valign='top'>
				259 
			</td>
		

			<td valign='top'>
				This group includes general activities for the treatment of metal, such as forging or pressing, which are typically carried out on a fee or contract basis. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398785
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.50 
			</td>
		

			<td valign='top'>
				25.5 
			</td>
		

			<td valign='top'>
				Forging, pressing, stamping and roll-forming of metal; powder metallurgy 
			</td>
		

			<td valign='top'>
				2591 
			</td>
		

			<td valign='top'>
				This class includes:- forging, pressing, stamping and roll-forming of metal- powder metallurgy: production of metal objects directly from metal powders by heat treatment (sintering) or under pressure 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of metal powder, see 24.1, 24.4 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398786
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.6 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Treatment and coating of metals; machining 
			</td>
		

			<td valign='top'>
				259 
			</td>
		

			<td valign='top'>
				This group includes general activities for the treatment of metal, such as plating, coating, engraving, boring, polishing, welding etc., which are typically carried out on a fee or contract basis. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398787
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.61 
			</td>
		

			<td valign='top'>
				25.6 
			</td>
		

			<td valign='top'>
				Treatment and coating of metals 
			</td>
		

			<td valign='top'>
				2592 
			</td>
		

			<td valign='top'>
				This class includes:- plating, anodising etc. of metals- heat treatment of metals- deburring, sandblasting, tumbling, cleaning of metals- colouring, engraving of metals- non-metallic coating of metals:  • plasticising, enamelling, lacquering etc.- hardening, buffing of metals 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of farriers, see 01.62- printing onto metals, see 18.12- metal coating of plastics, see 22.29- rolling precious metals onto base metals or other metals, see 24.41, 24.42, 24.43, 24.44- &quot;while-you-wait&quot; engraving services, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398788
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.62 
			</td>
		

			<td valign='top'>
				25.6 
			</td>
		

			<td valign='top'>
				Machining 
			</td>
		

			<td valign='top'>
				2592 
			</td>
		

			<td valign='top'>
				This class includes:- boring, turning, milling, eroding, planing, lapping, broaching, levelling, sawing, grinding, sharpening, polishing, welding, splicing etc. of metalwork pieces- cutting of and writing on metals by means of laser beams 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of farriers, see 01.62 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398789
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.7 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Manufacture of cutlery, tools and general hardware 
			</td>
		

			<td valign='top'>
				259 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of cutlery; metal hand tools and general hardware. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398790
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.71 
			</td>
		

			<td valign='top'>
				25.7 
			</td>
		

			<td valign='top'>
				Manufacture of cutlery 
			</td>
		

			<td valign='top'>
				2593 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of domestic cutlery such as knives, forks, spoons etc.- manufacture of other articles of cutlery:  • cleavers and choppers  • razors and razor blades  • scissors and hair clippers- manufacture of cutlasses, swords, bayonets etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of hollowware (pots, kettles etc.), dinnerware (bowls, platters etc.) or flatware (plates, saucers etc.), see 25.99- manufacture of cutlery of precious metal, see 32.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398791
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.72 
			</td>
		

			<td valign='top'>
				25.7 
			</td>
		

			<td valign='top'>
				Manufacture of locks and hinges 
			</td>
		

			<td valign='top'>
				2593 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of padlocks, locks, keys, hinges and the like, hardware for buildings, furniture, vehicles etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398792
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.73 
			</td>
		

			<td valign='top'>
				25.7 
			</td>
		

			<td valign='top'>
				Manufacture of tools 
			</td>
		

			<td valign='top'>
				2593 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of knives and cutting blades for machines or for mechanical appliances- manufacture of hand tools such as pliers, screwdrivers etc.- manufacture of non-power-driven agricultural hand tools- manufacture of saws and saw blades, including circular saw blades and chainsaw blades- manufacture of interchangeable tools for hand tools, whether or not power-operated, or for machine tools: drills, punches, milling cutters etc.- manufacture of press tools- manufacture of blacksmiths' tools: forges, anvils etc.- manufacture of moulding boxes and moulds (except ingot moulds)- manufacture of vices, clamps 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of power-driven hand tools, see 28.24- manufacture of ingot moulds, see 28.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398793
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				25.9 
			</td>
		

			<td valign='top'>
				25 
			</td>
		

			<td valign='top'>
				Manufacture of other fabricated metal products 
			</td>
		

			<td valign='top'>
				259 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of a variety of metal products, such as cans and buckets; nails, bolts and nuts; metal household articles; metal fixtures; ships propellers and anchors; assembled railway track fixtures etc. for a variety of household and industrial uses. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398794
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.91 
			</td>
		

			<td valign='top'>
				25.9 
			</td>
		

			<td valign='top'>
				Manufacture of steel drums and similar containers 
			</td>
		

			<td valign='top'>
				2599 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of pails, cans, drums, buckets, boxes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of tanks and reservoirs, see 25.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398795
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.92 
			</td>
		

			<td valign='top'>
				25.9 
			</td>
		

			<td valign='top'>
				Manufacture of light metal packaging 
			</td>
		

			<td valign='top'>
				2599 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of tins and cans for food products, collapsible tubes and boxes- manufacture of metallic closures 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398796
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.93 
			</td>
		

			<td valign='top'>
				25.9 
			</td>
		

			<td valign='top'>
				Manufacture of wire products, chain and springs 
			</td>
		

			<td valign='top'>
				2599 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of metal cable, plaited bands and similar articles- manufacture of uninsulated metal cable or insulated cable not capable of being used as a conductor of electricity- manufacture of coated or cored wire- manufacture of articles made of wire: barbed wire, wire fencing, grill, netting, cloth etc.- coated electrodes for electric arc-welding- manufacture of nails and pins- manufacture of springs (except watch springs):  • leaf springs, helical springs, torsion bar springs  • leaves for springs- manufacture of chain, except power transmission chain 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of clock or watch springs, see 26.52- manufacture of wire and cable for electricity transmission, see 27.32- manufacture of power transmission chain, see 28.15 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398797
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.94 
			</td>
		

			<td valign='top'>
				25.9 
			</td>
		

			<td valign='top'>
				Manufacture of fasteners and screw machine products 
			</td>
		

			<td valign='top'>
				2599 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of rivets, washers and similar non-threaded products- manufacture of screw machine products- manufacture of bolts, screws, nuts and similar threaded products 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398798
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				25.99 
			</td>
		

			<td valign='top'>
				25.9 
			</td>
		

			<td valign='top'>
				Manufacture of other fabricated metal products n.e.c. 
			</td>
		

			<td valign='top'>
				2599 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of metal household articles:  • flatware: plates, saucers etc.  • hollowware: pots, kettles etc.  • dinnerware: bowls, platters etc.  • saucepans, frying pans and other non-electrical utensils for use at the table or in the kitchen  • small hand-operated kitchen appliances and accessories  • metal scouring pads- manufacture of building components of zinc: gutters, roof capping, baths, sinks, washbasins and similar articles- manufacture of metal goods for office use, except furniture- manufacture of safes, strongboxes, armoured doors etc.- manufacture of various metal articles:  • ship propellers and blades thereof  • anchors  • bells  • assembled railway track fixtures  • clasps, buckles, hooks  • metal ladder  • metal signs, including road signs- manufacture of foil bags- manufacture of permanent metallic magnets- manufacture of metal vacuum jugs and bottles- manufacture of metal badges and metal military insignia- manufacture of metal hair curlers, metal umbrella handles and frames, combs 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of swords, bayonets, see 25.71- manufacture of shopping carts, see 30.99- manufacture of metal furniture, see 31.01, 31.02, 31.09- manufacture of sports goods, see 32.30- manufacture of games and toys, see 32.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398799
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of computer, electronic and optical products 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of computers, computer peripherals, communications equipment, and similar electronic products, as well as the manufacture of components for such products. Production processes of this division are characterised by the design and use of integrated circuits and the application of highly specialised miniaturisation technologies. 
			</td>
		

			<td valign='top'>
				The division also contains the manufacture of consumer electronics, measuring, testing and navigating equipment, irradiation, electromedical and electrotherapeutic equipment, optical instruments and equipment, and the manufacture of magnetic and optical media. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398800
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.1 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of electronic components and boards 
			</td>
		

			<td valign='top'>
				261 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398801
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.11 
			</td>
		

			<td valign='top'>
				26.1 
			</td>
		

			<td valign='top'>
				Manufacture of electronic components 
			</td>
		

			<td valign='top'>
				2610 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of semi-conductors and other components for electronic applications. This class includes:- manufacture of capacitors, electronic- manufacture of resistors, electronic- manufacture of microprocessors- manufacture of electron tubes- manufacture of electronic connectors- manufacture of bare printed circuit boards- manufacture of integrated circuits (analog, digital or hybrid)- manufacture of diodes, transistors and related discrete devices- manufacture of inductors (e.g. chokes, coils, transformers), electronic component type- manufacture of electronic crystals and crystal assemblies- manufacture of solenoids, switches and transducers for electronic applications- manufacture of dice or wafers, semi-conductor, finished or semi-finished- manufacture of display components (plasma, polymer, LCD)- manufacture of light emitting diodes (LED) 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of printer cables, monitor cables, USB cables, connectors etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- printing of smart cards, see 18.12- manufacture of computer and television displays, see 26.20, 26.40- manufacture of modems (carrier equipment), see 26.30- manufacture of X-ray tubes and similar irradiation devices, see 26.60- manufacture of optical equipment and instruments, see 26.70- manufacture of similar devices for electrical applications, see division 27- manufacture of fluorescent ballasts, see 27.11- manufacture of electrical relays, see 27.12- manufacture of electrical wiring devices, see 27.33- manufacture of complete equipment is classified elsewhere based on complete equipment classification 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398802
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.12 
			</td>
		

			<td valign='top'>
				26.1 
			</td>
		

			<td valign='top'>
				Manufacture of loaded electronic boards 
			</td>
		

			<td valign='top'>
				2610 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of loaded printed circuit boards- loading of components onto printed circuit boards- manufacture of interface cards (e.g. sound, video, controllers, network, modems) 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- printing of smart cards, see 18.12- manufacture of bare printed circuit boards, see 26.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398803
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.2 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of computers and peripheral equipment 
			</td>
		

			<td valign='top'>
				262 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398804
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.20 
			</td>
		

			<td valign='top'>
				26.2 
			</td>
		

			<td valign='top'>
				Manufacture of computers and peripheral equipment 
			</td>
		

			<td valign='top'>
				2620 
			</td>
		

			<td valign='top'>
				This class includes the manufacture and/or assembly of electronic computers, such as mainframes, desktop computers, laptops and computer servers; and computer peripheral equipment, such as storage devices and input/output devices (printers, monitors, keyboards). Computers can be analog, digital, or hybrid. Digital computers, the most common type, are devices that do all of the following: (1) store the processing program or programs and the data immediately necessary for the execution of the program, (2) can be freely programmed in accordance with the requirements of the user, (3) perform arithmetical computations specified by the user and (4) execute, without human intervention, a processing program that requires the computer to modify its execution by logical decision during the processing run. Analog computers are capable of simulating mathematical models and comprise at least analog control and programming elements.This class includes:- manufacture of desktop computers- manufacture of laptop computers- manufacture of main frame computers- manufacture of hand-held computers (e.g. PDA)- manufacture of magnetic disk drives, flash drives and other storage devices- manufacture of optical (e.g. CD-RW, CD-ROM, DVD-ROM, DVD-RW) disk drives- manufacture of printers- manufacture of monitors- manufacture of keyboards- manufacture of all types of mice, joysticks, and trackball accessories- manufacture of dedicated computer terminals- manufacture of computer servers- manufacture of scanners, including bar code scanners- manufacture of smart card readers- manufacture of virtual reality helmets- manufacture of computer projectors (video beamers) 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of computer terminals, like automatic teller machines (ATM's), point-of-sale (POS) terminals, not mechanically operated- manufacture of multi-function office equipment performing two or more of following functions: printing, scanning, copying, faxing 
			</td>
		

			<td valign='top'>
				- Refilling of ink cartridges 
			</td>
		

			<td valign='top'>
				This class excludes:- reproduction of recorded media (computer media, sound, video, etc.), see 18.20- manufacture of electronic components and electronic assemblies used in computers and peripherals, see 26.1- manufacture of internal/external computer modems, see 26.12- manufacture of interface cards, modules and assemblies, see 26.12- manufacture of loaded electronic boards, see 26.12- manufacture of modems, carrier equipment, see 26.30- manufacture of digital communication switches, data communications equipment (e.g. bridges, routers, gateways), see 26.30- manufacture of consumer electronic devices, such as CD players and DVD players, see 26.40- manufacture of television monitors and displays, see 26.40- manufacture of video game consoles, see 26.40- manufacture of blank optical and magnetic media for use with computers or other devices, see 26.80 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398805
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.3 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of communication equipment 
			</td>
		

			<td valign='top'>
				263 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398806
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.30 
			</td>
		

			<td valign='top'>
				26.3 
			</td>
		

			<td valign='top'>
				Manufacture of communication equipment 
			</td>
		

			<td valign='top'>
				2630 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of telephone and data communications equipment used to move signals electronically over wires or through the air such as radio and television broadcast and wireless communications equipment.This class includes:- manufacture of central office switching equipment- manufacture of cordless telephones- manufacture of private branch exchange (PBX) equipment- manufacture of telephone and facsimile equipment, including telephone answering machines- manufacture of data communications equipment, such as bridges, routers, and gateways- manufacture of transmitting and receiving antenna- manufacture of cable television equipment- manufacture of pagers- manufacture of cellular phones- manufacture of mobile communication equipment- manufacture of radio and television studio and broadcasting equipment, including television cameras- manufacture of modems, carrier equipment- manufacture of burglar and fire alarm systems, sending signals to a control station- manufacture of radio and television transmitters- manufacture of communication devices using infrared signal (e.g. remote controls) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of incomplete plasma monitors 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of electronic components and subassemblies used in communications equipment, including internal/external computer modems, see 26.1- manufacture of loaded electronic boards, see 26.12- manufacture of computers and computer peripheral equipment, see 26.20- manufacture of consumer audio and video equipment, see 26.40- manufacture of GPS devices, see 26.51- manufacture of electronic scoreboards, see 27.90- manufacture of traffic lights, see 27.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398807
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.4 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of consumer electronics 
			</td>
		

			<td valign='top'>
				264 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398808
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.40 
			</td>
		

			<td valign='top'>
				26.4 
			</td>
		

			<td valign='top'>
				Manufacture of consumer electronics 
			</td>
		

			<td valign='top'>
				2640 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of electronic audio and video equipment for home entertainment, motor vehicle, public address systems and musical instrument amplification. This class includes:- manufacture of video cassette recorders and duplicating equipment- manufacture of televisions- manufacture of television monitors and displays- manufacture of audio recording and duplicating systems- manufacture of stereo equipment- manufacture of radio receivers- manufacture of speaker systems- manufacture of household-type video cameras- manufacture of jukeboxes- manufacture of amplifiers for musical instruments and public address systems- manufacture of microphones- manufacture of CD and DVD players- manufacture of karaoke machines- manufacture of headphones (e.g. radio, stereo, computer)- manufacture of video game consoles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- reproduction of recorded media (computer media, sound, video, etc.), see 18.2- manufacture of computer peripheral devices and computer monitors, see 26.20- manufacture of telephone answering machines, see 26.30- manufacture of paging equipment, see 26.30- manufacture of remote control devices (radio and infrared), see 26.30- manufacture of broadcast studio equipment such as reproduction equipment, transmitting and receiving antennas, commercial video cameras, see 26.30 - manufacture of antennas, see 26.30- manufacture of digital cameras, see 26.70- manufacture of electronic games with fixed (non-replaceable) software, see 32.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398809
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.5 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of instruments and appliances for measuring, testing and navigation; watches and clocks 
			</td>
		

			<td valign='top'>
				265 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of measuring, testing and navigating equipment for various industrial and non-industrial purposes, including time-based measuring devices such as watches and clocks and related devices. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398810
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.51 
			</td>
		

			<td valign='top'>
				26.5 
			</td>
		

			<td valign='top'>
				Manufacture of instruments and appliances for measuring, testing and navigation 
			</td>
		

			<td valign='top'>
				2651 
			</td>
		

			<td valign='top'>
				This class comprises manufacture of search, detection, navigation, guidance, aeronautical, and nautical systems and instruments; automatic controls and regulators for applications, such as heating, air-conditioning, refrigeration and appliances; instruments and devices for measuring, displaying, indicating, recording, transmitting, and controlling temperature, humidity, pressure, vacuum, combustion, flow, level, viscosity, density, acidity, concentration, and rotation; totalising (i.e., registering) fluid meters and counting devices; instruments for measuring and testing the characteristics of electricity and electrical signals; instruments and instrumentation systems for laboratory analysis of the chemical or physical composition or concentration of samples of solid, fluid, gaseous, or composite material; other measuring and testing instruments and parts thereof.The manufacture of non-electric measuring, testing and navigating equipment (except simple mechanical tools) is included here.This class includes:- manufacture of aircraft engine instruments- manufacture of automotive emissions testing equipment- manufacture of meteorological instruments- manufacture of physical properties testing and inspection equipment- manufacture of polygraph machines- manufacture of radiation detection and monitoring instruments- manufacture of surveying instruments- manufacture of thermometers liquid-in-glass and bimetal types (except medical)- manufacture of humidistats- manufacture of hydronic limit controls- manufacture of flame and burner control- manufacture of spectrometers- manufacture of pneumatic gauges- manufacture of consumption meters (e.g., water, gas, electricity)- manufacture of flow meters and counting devices- manufacture of tally counters- manufacture of mine detectors, pulse (signal) generators; metal detectors- manufacture of search, detection, navigation, aeronautical, and nautical equipment, including sonobuoys- manufacture of radar equipment- manufacture of GPS devices- manufacture of environmental controls and automatic controls for appliances- manufacture of measuring and recording equipment (e.g. flight recorders)- manufacture of motion detectors- manufacture of radars- manufacture of laboratory analytical instruments (e.g. blood analysis equipment) - manufacture of laboratory scales, balances, incubators, and miscellaneous laboratory apparatus for measuring, testing, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of telephone answering machines, see 26.30- manufacture of irradiation equipment, see 26.60- manufacture of optical positioning equipment, see 26.70- manufacture of dictating machines, see 28.23- manufacture of weighing machinery (other than sensitive laboratory balances), levels, tape measures etc., see 28.29- manufacture of medical thermometers, see 32.50- installation of industrial process control equipment, see 33.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398811
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.52 
			</td>
		

			<td valign='top'>
				26.5 
			</td>
		

			<td valign='top'>
				Manufacture of watches and clocks 
			</td>
		

			<td valign='top'>
				2652 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of watches, clocks and timing mechanisms and parts thereof.This class includes:- manufacture of watches and clocks of all kinds, including instrument panel clocks- manufacture of watch and clock cases, including cases of precious metals- manufacture of time-recording equipment and equipment for measuring, recording and otherwise displaying intervals of time with a watch or clock movement or with synchronous motor, such as:  • parking meters  • time clocks  • time/date stamps  • process timers- manufacture of time switches and other releases with a watch or clock movement or with synchronous motor:  • time locks- manufacture of components for clocks and watches:   • movements of all kinds for watches and clocks  • springs, jewels, dials, hands, plates, bridges and other parts  • watch and clock cases and housings of all materials 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of non-metal watch bands (textile, leather, plastic), see 15.12- manufacture of watch bands of precious metal, see 32.12- manufacture of watch bands of non-precious metal, see 32.13 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398812
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.6 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of irradiation, electromedical and electrotherapeutic equipment 
			</td>
		

			<td valign='top'>
				266 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398813
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.60 
			</td>
		

			<td valign='top'>
				26.6 
			</td>
		

			<td valign='top'>
				Manufacture of irradiation, electromedical and electrotherapeutic equipment 
			</td>
		

			<td valign='top'>
				2660 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of irradiation apparatus and tubes (e.g. industrial, medical diagnostic, medical therapeutic, research, scientific):  • beta-, gamma, X-ray or other radiation equipment- manufacture of CT scanners- manufacture of PET scanners- manufacture of magnetic resonance imaging (MRI) equipment- manufacture of medical ultrasound equipment- manufacture of electrocardiographs- manufacture of electromedical endoscopic equipment- manufacture of medical laser equipment- manufacture of pacemakers- manufacture of hearing aids 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of food and milk irradiation equipment 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of tanning beds, see 27.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398814
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.7 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of optical instruments and photographic equipment 
			</td>
		

			<td valign='top'>
				267 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398815
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.70 
			</td>
		

			<td valign='top'>
				26.7 
			</td>
		

			<td valign='top'>
				Manufacture of optical instruments and photographic equipment 
			</td>
		

			<td valign='top'>
				2670 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of optical instruments and lenses, such as binoculars, microscopes (except electron, proton), telescopes, prisms and lenses (except ophthalmic); the coating or polishing of lenses (except ophthalmic); the mounting of lenses (except ophthalmic) and the manufacture of photographic equipment such as cameras and light meters.This class includes:- manufacture of optical mirrors- manufacture of optical gun sighting equipment- manufacture of optical positioning equipment- manufacture of optical magnifying instruments- manufacture of optical machinist's precision tools- manufacture of optical comparators- manufacture of film cameras and digital cameras- manufacture of motion picture and slide projectors- manufacture of overhead transparency projectors- manufacture of optical measuring and checking devices and instruments (e.g. fire control equipment, photographic light meters, range finders)- manufacture of lenses, optical microscopes, binoculars and telescopes- manufacture of laser assemblies 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of computer projectors, see 26.20- manufacture of commercial TV and video cameras, see 26.30- manufacture of household-type video cameras, see 26.40- manufacture of complete equipment using laser components, see manufacturing class by type of machinery (e.g. medical laser equipment, see 26.60)- manufacture of photocopy machinery, see 28.23- manufacture of ophthalmic goods, see 32.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398816
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				26.8 
			</td>
		

			<td valign='top'>
				26 
			</td>
		

			<td valign='top'>
				Manufacture of magnetic and optical media 
			</td>
		

			<td valign='top'>
				268 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398817
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				26.80 
			</td>
		

			<td valign='top'>
				26.8 
			</td>
		

			<td valign='top'>
				Manufacture of magnetic and optical media 
			</td>
		

			<td valign='top'>
				2680 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of magnetic and optical recording media.This class includes:- manufacture of blank magnetic audio and video tapes- manufacture of blank magnetic audio and video cassettes- manufacture of blank diskettes- manufacture of blank optical discs- manufacture of hard drive media 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- reproduction of recorded media (computer media, sound, video, etc.), see 18.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398818
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of electrical equipment 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of products that generate, distribute and use electrical power. 
			</td>
		

			<td valign='top'>
				Also included is the manufacture of electrical lighting, signalling equipment and electric household appliances. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes the manufacture of electronic products (see division 26). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398819
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				27.1 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				Manufacture of electric motors, generators, transformers and electricity distribution and control apparatus 
			</td>
		

			<td valign='top'>
				271 
			</td>
		

			<td valign='top'>
				This group comprises the manufacture of power, distribution and specialty transformers; electric motors, generators, and motor generator sets. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398820
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.11 
			</td>
		

			<td valign='top'>
				27.1 
			</td>
		

			<td valign='top'>
				Manufacture of electric motors, generators and transformers 
			</td>
		

			<td valign='top'>
				2710 
			</td>
		

			<td valign='top'>
				This class includes manufacture of all electric motors and transformers: AC, DC and AC/DC.This class includes:- manufacture of electric motors (except internal combustion engine starting motors)- manufacture of distribution transformers, electric- manufacture of arc-welding transformers- manufacture of fluorescent ballasts (i.e. transformers)- manufacture of substation transformers for electric power distribution- manufacture of transmission and distribution voltage regulators- manufacture of power generators (except battery charging alternators for internal combustion engines)- manufacture of motor generator sets (except turbine generator set units)- rewinding of armatures on a factory basis 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of photovoltaic solar generators 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of electronic component-type transformers and switches, see 26.11- manufacture of electric welding and soldering equipment, see 27.90- manufacture of solid state inverters, rectifiers and converters, see 27.90- manufacture of turbine-generator sets, see 28.11- manufacture of starting motors and generators for internal combustion engines, see 29.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398821
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.12 
			</td>
		

			<td valign='top'>
				27.1 
			</td>
		

			<td valign='top'>
				Manufacture of electricity distribution and control apparatus 
			</td>
		

			<td valign='top'>
				2710 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of power circuit breakers- manufacture of surge suppressors (for distribution level voltage)- manufacture of control panels for electric power distribution- manufacture of electrical relays- manufacture of duct for electrical switchboard apparatus- manufacture of electric fuses- manufacture of power switching equipment- manufacture of electric power switches (except pushbutton, snap, solenoid, tumbler)- manufacture of prime mover generator sets 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of environmental controls and industrial process control instruments, see 26.51- manufacture of switches for electrical circuits, such as pushbutton and snap switches, see 27.33 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398822
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				27.2 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				Manufacture of batteries and accumulators 
			</td>
		

			<td valign='top'>
				272 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398823
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.20 
			</td>
		

			<td valign='top'>
				27.2 
			</td>
		

			<td valign='top'>
				Manufacture of batteries and accumulators 
			</td>
		

			<td valign='top'>
				2720 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of non-rechargeable and rechargeable batteries.This class includes:- manufacture of primary cells and primary batteries   • cells containing manganese dioxide, mercuric dioxide, silver oxide etc.- manufacture of electric accumulators, including parts thereof:  • separators, containers, covers- manufacture of lead acid batteries- manufacture of NiCad batteries- manufacture of NiMH batteries- manufacture of lithium batteries- manufacture of dry cell batteries- manufacture of wet cell batteries 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398824
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				27.3 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				Manufacture of wiring and wiring devices 
			</td>
		

			<td valign='top'>
				273 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of current-carrying wiring devices and non current-carrying wiring devices for wiring electrical circuits regardless of material. 
			</td>
		

			<td valign='top'>
				This group also includes the insulating of wire and the manufacture of fibre optic cables. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398825
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.31 
			</td>
		

			<td valign='top'>
				27.3 
			</td>
		

			<td valign='top'>
				Manufacture of fibre optic cables 
			</td>
		

			<td valign='top'>
				2731 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of fibre optic cable for data transmission or live transmission of images 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of glass fibres or strand, see 23.14- manufacture of optical cable sets or assemblies with connectors or other attachments, see depending on application, e.g. 26.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398826
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.32 
			</td>
		

			<td valign='top'>
				27.3 
			</td>
		

			<td valign='top'>
				Manufacture of other electronic and electric wires and cables 
			</td>
		

			<td valign='top'>
				2732 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of insulated wire and cable, made of steel, copper, aluminium 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture (drawing) of wire, see 24.34, 24.41, 24.42, 24.43, 24.44, 24.45- manufacture of computer cables, printer cables, USB cables and similar cable sets or assemblies, see 26.11- manufacture of electrical cord sets with insulated wire and connectors, see 27.90- manufacture of cable sets, wiring harnesses and similar cable sets or assemblies for automotive applications, see 29.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398827
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.33 
			</td>
		

			<td valign='top'>
				27.3 
			</td>
		

			<td valign='top'>
				Manufacture of wiring devices 
			</td>
		

			<td valign='top'>
				2733 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of current-carrying and non current-carrying wiring devices for electrical circuits regardless of material.This class includes:- manufacture of bus bars, electrical conductors (except switchgear-type) - manufacture of GFCI (ground fault circuit interrupters) - manufacture of lamp holders - manufacture of lightning arrestors and coils- manufacture of switches for electrical wiring (e.g. pressure, pushbutton, snap, tumbler switches) - manufacture of electrical outlets or sockets- manufacture of boxes for electrical wiring (e.g. junction, outlet, switch boxes) - manufacture of electrical conduit and fitting - manufacture of transmission pole and line hardware - manufacture of plastic non current carrying wiring devices including plastic junction boxes, face plates and similar, plastic pole line fittings and switch covers 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of ceramic insulators, see 23.43- manufacture of electronic component-type connectors, sockets, and switches, see 26.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398828
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				27.4 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				Manufacture of electric lighting equipment 
			</td>
		

			<td valign='top'>
				274 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398829
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.40 
			</td>
		

			<td valign='top'>
				27.4 
			</td>
		

			<td valign='top'>
				Manufacture of electric lighting equipment 
			</td>
		

			<td valign='top'>
				2740 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of electric light bulbs and tubes and parts and components thereof (except glass blanks for electric light bulbs), electric lighting fixtures and lighting fixture components (except current-carrying wiring devices).This class includes:- manufacture of discharge, incandescent, fluorescent, ultra-violet, infra-red etc. lamps, fixtures and bulbs- manufacture of ceiling lighting fixtures- manufacture of chandeliers- manufacture of table lamps (i.e. lighting fixture)- manufacture of Christmas tree lighting sets - manufacture of electric fireplace logs - manufacture of flashlights- manufacture of electric insect lamps- manufacture of lanterns (e.g. carbide, electric, gas, gasoline, kerosene)- manufacture of spotlights- manufacture of street lighting fixtures (except traffic signals)- manufacture of lighting equipment for transportation equipment (e.g. for motor vehicles, aircraft, boats) 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of non-electrical lighting equipment 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of glassware and glass parts for lighting fixtures, see 23.19- manufacture of current-carrying wiring devices for lighting fixtures, see 27.33- manufacture of ceiling fans or bath fans with integrated lighting fixtures, see 27.51- manufacture of electrical signalling equipment such as traffic lights and pedestrian signalling equipment, see 27.90- manufacture of electrical signs, see 27.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398830
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				27.5 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				Manufacture of domestic appliances 
			</td>
		

			<td valign='top'>
				275 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of small electric appliances and electric housewares, household-type fans, household-type vacuum cleaners, electric household-type floor care machines, household-type cooking appliances, household-type laundry equipment, household-type refrigerators, upright and chest freezers, and other electrical and non-electrical household appliances, such as dishwashers, water heaters, and garbage disposal units. This group includes the manufacture of appliances with electric, gas or other fuel sources. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398831
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.51 
			</td>
		

			<td valign='top'>
				27.5 
			</td>
		

			<td valign='top'>
				Manufacture of electric domestic appliances 
			</td>
		

			<td valign='top'>
				2750 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of domestic electric appliances:  • refrigerators   • freezers  • dishwashers  • washing and drying machines  • vacuum cleaners  • floor polishers  • waste disposers  • grinders, blenders, juice squeezers  • tin openers  • electric shavers, electric toothbrushes, and other electric personal care device  • knife sharpeners  • ventilating or recycling hoods- manufacture of domestic electrothermic appliances:  • electric water heaters  • electric blankets  • electric dryers, combs, brushes, curlers  • electric smoothing irons  • space heaters and household-type fans, portable  • electric ovens  • microwave ovens  • cookers, hotplates  • toasters  • coffee or tea makers  • fry pans, roasters, grills, hoods  • electric heating resistors etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of commercial and industrial refrigerators and freezers, room air-conditioners, attic fans, permanent mount space heaters, and commercial ventilation and exhaust fans, commercial-type cooking equipment; commercial-type laundry, dry-cleaning, and pressing equipment; commercial, industrial, and institutional vacuum cleaners, see division 28- manufacture of household-type sewing machines, see 28.94- installation of central vacuum cleaning systems, 43.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398832
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.52 
			</td>
		

			<td valign='top'>
				27.5 
			</td>
		

			<td valign='top'>
				Manufacture of non-electric domestic appliances 
			</td>
		

			<td valign='top'>
				2750 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of domestic non-electric cooking and heating equipment:  • non-electric space heaters, cooking ranges, grates, stoves, water heaters, cooking appliances, plate warmers 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398833
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				27.9 
			</td>
		

			<td valign='top'>
				27 
			</td>
		

			<td valign='top'>
				Manufacture of other electrical equipment 
			</td>
		

			<td valign='top'>
				279 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398834
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				27.90 
			</td>
		

			<td valign='top'>
				27.9 
			</td>
		

			<td valign='top'>
				Manufacture of other electrical equipment 
			</td>
		

			<td valign='top'>
				2790 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of miscellaneous electrical equipment other than motors, generators and transformers, batteries and accumulators, wires and wiring devices, lighting equipment or domestic appliances.This class includes:- manufacture of battery chargers, solid-state- manufacture of door opening and closing devices, electrical- manufacture of electric bells- manufacture of extension cords made from purchased insulated wire- manufacture of ultrasonic cleaning machines (except laboratory and dental)- manufacture of tanning beds- manufacture of solid state inverters, rectifying apparatus, fuel cells, regulated and unregulated power supplies- manufacture of uninterruptible power supplies (UPS)- manufacture of surge suppressors (except for distribution level voltage)- manufacture of appliance cords, extension cords, and other electrical cord sets with insulated wire and connectors- manufacture of carbon and graphite electrodes, contacts, and other electrical carbon and graphite products - manufacture of particle accelerators- manufacture of electrical capacitors, resistors, condensers and similar components- manufacture of electromagnets- manufacture of sirens- manufacture of electronic scoreboards- manufacture of electrical signs- manufacture of electrical signalling equipment such as traffic lights and pedestrian signalling equipment- manufacture of electrical insulators (except glass or porcelain)- manufacture of electrical welding and soldering equipment, including hand-held soldering irons 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of porcelain electrical insulators, see 23.43- manufacture of carbon and graphite fibres and products (except electrodes and electrical applications), see 23.99- manufacture of electronic component-type rectifiers, voltage regulating integrated circuits, power converting integrated circuits, electronic capacitors, electronic resistors, and similar devices, see 26.11- manufacture of transformers, motors, generators, switchgear, relays and industrial controls, see 27.1- manufacture of batteries, see 27.20- manufacture of communication and energy wire, current-carrying and non current-carrying wiring devices, see 27.3- manufacture of lighting equipment, see 27.40- manufacture of household-type appliances, see 27.5- manufacture of non-electrical welding and soldering equipment, see 28.29- manufacture of motor vehicle electrical equipment, such as generators, alternators, spark plugs, ignition wiring harnesses, power window and door systems, voltage regulators, see 29.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398835
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of machinery and equipment n.e.c. 
			</td>
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of machinery and equipment that act independently on materials either mechanically or thermally or perform operations on materials (such as handling, spraying, weighing or packing), including their mechanical components that produce and apply force, and any specially manufactured primary parts. This includes the manufacture of fixed and mobile or hand-held devices, regardless of whether they are designed for industrial, building and civil engineering, agricultural or home use. The manufacture of special equipment for passenger or freight transport within demarcated premises also belongs within this division.This division distinguishes between the manufacture of special-purpose machinery, i.e. machinery for exclusive use in a NACE industry or a small cluster of NACE industries, and general-purpose machinery, i.e. machinery that is being used in a wide range of NACE industries. 
			</td>
		

			<td valign='top'>
				This division also includes the manufacture of other special-purpose machinery, not covered elsewhere in the classification, whether or not used in a manufacturing process, such as fairground amusement equipment, automatic bowling alley equipment, etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes the manufacture of metal products for general use (division 25), associated control devices, computer equipment, measurement and testing equipment, electricity distribution and control apparatus (divisions 26 and 27) and general-purpose motor vehicles (divisions 29 and 30). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398836
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				28.1 
			</td>
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				Manufacture of general-purpose machinery 
			</td>
		

			<td valign='top'>
				281 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398837
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.11 
			</td>
		

			<td valign='top'>
				28.1 
			</td>
		

			<td valign='top'>
				Manufacture of engines and turbines, except aircraft, vehicle and cycle engines 
			</td>
		

			<td valign='top'>
				2811 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of internal combustion piston engines, except motor vehicle, aircraft and cycle propulsion engines:  • marine engines  • railway engines- manufacture of pistons, piston rings, carburettors and such for all internal combustion engines, diesel engines etc.- manufacture of inlet and exhaust valves of internal combustion engines- manufacture of turbines and parts thereof:  • steam turbines and other vapour turbines  • hydraulic turbines, waterwheels and regulators thereof  • wind turbines  • gas turbines, except turbojets or turbo propellers for aircraft propulsion - manufacture of boiler-turbine sets- manufacture of turbine-generator sets- manufacture of engines for industrial application 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of electric generators (except turbine generator sets), see 27.11- manufacture of prime mover generator sets (except turbine generator sets), see 27.11- manufacture of electrical equipment and components of internal combustion engines, see 29.31- manufacture of motor vehicle, aircraft or cycle propulsion engines, see 29.10, 30.30, 30.91- manufacture of turbojets and turbo propellers, see 30.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398838
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.12 
			</td>
		

			<td valign='top'>
				28.1 
			</td>
		

			<td valign='top'>
				Manufacture of fluid power equipment 
			</td>
		

			<td valign='top'>
				2812 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of hydraulic and pneumatic components (including hydraulic pumps, hydraulic motors, hydraulic and pneumatic cylinders, hydraulic and pneumatic valves, hydraulic and pneumatic hose and fittings)- manufacture of air preparation equipment for use in pneumatic systems- manufacture of fluid power systems- manufacture of hydraulic transmission equipment- manufacture of hydrostatic transmissions 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of compressors, see 28.13- manufacture of pumps for non-hydraulic applications, see 28.13- manufacture of valves for non-fluid power applications, see 28.14- manufacture of mechanical transmission equipment, see 28.15 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398839
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.13 
			</td>
		

			<td valign='top'>
				28.1 
			</td>
		

			<td valign='top'>
				Manufacture of other pumps and compressors 
			</td>
		

			<td valign='top'>
				2813 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of air or vacuum pumps, air or other gas compressors- manufacture of pumps for liquids whether or not fitted with a measuring device- manufacture of pumps designed for fitting to internal combustion engines: oil, water and fuel pumps for motor vehicles etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of hand pumps 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of hydraulic and pneumatic equipment, see 28.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398840
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.14 
			</td>
		

			<td valign='top'>
				28.1 
			</td>
		

			<td valign='top'>
				Manufacture of other taps and valves 
			</td>
		

			<td valign='top'>
				2813 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of industrial taps and valves, including regulating valves and intake taps- manufacture of sanitary taps and valves- manufacture of heating taps and valves 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of valves of unhardened vulcanised rubber, glass or of ceramic materials, see 22.19, 23.19 or 23.44- manufacture of inlet and exhaust valves of internal combustion engines, see 28.11- manufacture of hydraulic and pneumatic valves and air preparation equipment for use in pneumatic systems, see 28.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398841
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.15 
			</td>
		

			<td valign='top'>
				28.1 
			</td>
		

			<td valign='top'>
				Manufacture of bearings, gears, gearing and driving elements 
			</td>
		

			<td valign='top'>
				2814 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of ball and roller bearings and parts thereof- manufacture of mechanical power transmission equipment:  • transmission shafts and cranks: camshafts, crankshafts, cranks etc.  • bearing housings and plain shaft bearings- manufacture of gears, gearing and gear boxes and other speed changers- manufacture of clutches and shaft couplings- manufacture of flywheels and pulleys- manufacture of articulated link chain- manufacture of power transmission chain 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of other chain, see 25.93- manufacture of hydraulic transmission equipment, see 28.12- manufacture of hydrostatic transmissions, see 28.12- manufacture of (electromagnetic) clutches, see 29.31- manufacture of sub-assemblies of power transmission equipment identifiable as parts of vehicles or aircraft, see divisions 29 and 30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398842
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				Manufacture of other general-purpose machinery 
			</td>
		

			<td valign='top'>
				281 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398843
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.21 
			</td>
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				Manufacture of ovens, furnaces and furnace burners 
			</td>
		

			<td valign='top'>
				2815 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of electrical and other industrial and laboratory furnaces and ovens, including incinerators- manufacture of burners- manufacture of permanent mount electric space heaters, electric swimming pool heaters- manufacture of permanent mount non-electric household heating equipment, such as solar heating, steam heating, oil heat and similar furnaces and heating equipment- manufacture of electric household-type furnaces (electric forced air furnaces, heat pumps, etc.), non-electric household forced air furnaces 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of mechanical stokers, grates, ash dischargers etc. 
			</td>
		

			<td valign='top'>
				- Manufacture of solar heating collectors 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of household ovens, see 27.51- manufacture of agricultural dryers, see 28.93- manufacture of bakery ovens, see 28.93- manufacture of dryers for wood, paper pulp, paper or paperboard, see 28.99- manufacture of medical, surgical or laboratory sterilisers, see 32.50- manufacture of (dental) laboratory furnaces, see 32.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398844
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.22 
			</td>
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				Manufacture of lifting and handling equipment 
			</td>
		

			<td valign='top'>
				2816 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of hand-operated or power-driven lifting, handling, loading or unloading machinery:  • pulley tackle and hoists, winches, capstans and jacks  • derricks, cranes, mobile lifting frames, straddle carriers etc.  • works trucks, whether or not fitted with lifting or handling equipment, whether or not self-propelled, of the type used in factories (including hand trucks and wheelbarrows)  • mechanical manipulators and industrial robots specifically designed for lifting, handling, loading or unloading- manufacture of conveyors, teleferics etc.- manufacture of lifts, escalators and moving walkways- manufacture of parts specialised for lifting and handling equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of industrial robots for multiple uses, see 28.99- manufacture of continuous-action elevators and conveyors for underground use, see 28.92- manufacture of mechanical shovels, excavators and shovel loaders, see 28.92- manufacture of floating cranes, railway cranes, crane-lorries, see 30.11, 30.20- installation of lifts and elevators, see 43.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398845
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.23 
			</td>
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				Manufacture of office machinery and equipment (except computers and peripheral equipment) 
			</td>
		

			<td valign='top'>
				2817 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of calculating machines- manufacture of adding machines, cash registers- manufacture of calculators, electronic or not- manufacture of postage meters, mail handling machines (envelope stuffing, sealing and addressing machinery; opening, sorting, scanning), collating machinery- manufacture of typewriters- manufacture of stenography machines- manufacture of office-type binding equipment (i.e. plastic or tape binding) - manufacture of cheque writing machines- manufacture of coin counting and coin wrapping machinery- manufacture of pencil sharpeners- manufacture of staplers and staple removers- manufacture of voting machines- manufacture of tape dispensers- manufacture of hole punches- manufacture of cash registers, mechanically operated- manufacture of photocopy machines- manufacture of toner cartridges- manufacture of blackboards; white boards and marker boards- manufacture of dictating machines 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of computers and peripheral equipment, see 26.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398846
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.24 
			</td>
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				Manufacture of power-driven hand tools 
			</td>
		

			<td valign='top'>
				2818 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of hand tools, with self-contained electric or non-electric motor or pneumatic drive, such as:  • circular or reciprocating saws  • chain saws  • drills and hammer drills  • hand held power sanders  • pneumatic nailers  • buffers  • routers  • grinders  • staplers  • pneumatic rivet guns  • planers  • shears and nibblers  • impact wrenches  • powder actuated nailers 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of interchangeable tools for hand tools, see 25.73- manufacture of electrical hand held soldering and welding equipment, see 27.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398847
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.25 
			</td>
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				Manufacture of non-domestic cooling and ventilation equipment 
			</td>
		

			<td valign='top'>
				2819 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of refrigerating or freezing industrial equipment, including assemblies of components- manufacture of air-conditioning machines, including for motor vehicles- manufacture of non-domestic fans- manufacture of heat exchangers- manufacture of machinery for liquefying air or gas- manufacture of attic ventilation fans (gable fans, roof ventilators, etc.) 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of domestic refrigerating or freezing equipment, see 27.51- manufacture of domestic fans, see 27.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398848
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.29 
			</td>
		

			<td valign='top'>
				28.2 
			</td>
		

			<td valign='top'>
				Manufacture of other general-purpose machinery n.e.c. 
			</td>
		

			<td valign='top'>
				2819 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of weighing machinery (other than sensitive laboratory balances):  • household and shop scales, platform scales, scales for continuous weighing, weighbridges, weights etc.- manufacture of filtering or purifying machinery and apparatus for liquids- manufacture of equipment for projecting, dispersing or spraying liquids or powders:  • spray guns, fire extinguishers, sandblasting machines, steam cleaning machines etc.- manufacture of packing and wrapping machinery:  • filling, closing, sealing, capsuling or labelling machines etc.- manufacture of machinery for cleaning or drying bottles and for aerating beverages- manufacture of distilling or rectifying plant for petroleum refineries, chemical industries, beverage industries etc.- manufacture of gas generators- manufacture of calendaring or other rolling machines and cylinders thereof (except for metal and glass)- manufacture of centrifuges (except cream separators and clothes dryers)- manufacture of gaskets and similar joints made of a combination of materials or layers of the same material- manufacture of automatic goods vending machines- manufacture of levels, tape measures and similar hand tools, machinists' precision tools (except optical)- manufacture of non-electrical welding and soldering equipment- manufacture of cooling towers and similar for direct cooling by means of re-circulated water 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of sensitive (laboratory-type) balances, see 26.51- manufacture of domestic refrigerating or freezing equipment, see 27.51- manufacture of domestic fans, see 27.51- manufacture of electrical welding and soldering equipment, see 27.90- manufacture of agricultural spraying machinery, see 28.30- manufacture of metal or glass rolling machinery and cylinders thereof, see 28.91, 28.99- manufacture of agricultural dryers, see 28.93- manufacture of machinery for filtering or purifying food, see 28.93- manufacture of cream separators, see 28.93- manufacture of commercial clothes dryers, see 28.94- manufacture of textile printing machinery, see 28.94 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398849
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				28.3 
			</td>
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				Manufacture of agricultural and forestry machinery 
			</td>
		

			<td valign='top'>
				282 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398850
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.30 
			</td>
		

			<td valign='top'>
				28.3 
			</td>
		

			<td valign='top'>
				Manufacture of agricultural and forestry machinery 
			</td>
		

			<td valign='top'>
				2821 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of tractors used in agriculture and forestry- manufacture of walking (pedestrian-controlled) tractors- manufacture of mowers, including lawnmowers- manufacture of agricultural self-loading or self-unloading trailers or semi-trailers- manufacture of agricultural machinery for soil preparation, planting or fertilising:  • ploughs, manure spreaders, seeders, harrows etc.- manufacture of harvesting or threshing machinery:  • harvesters, threshers, sorters etc.- manufacture of milking machines- manufacture of spraying machinery for agricultural use- manufacture of diverse agricultural machinery:  • poultry-keeping machinery, bee-keeping machinery, equipment for preparing fodder etc.  • machines for cleaning, sorting or grading eggs, fruit etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of non-power-driven agricultural hand tools, see 25.73- manufacture of conveyors for farm use, see 28.22- manufacture of power-driven hand tools, see 28.24- manufacture of cream separators, see 28.93- manufacture of machinery to clean, sort or grade seed, grain or dried leguminous vegetables, see 28.93- manufacture of road tractors for semi-trailers, see 29.10- manufacture of road trailers or semi-trailers, see 29.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398851
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				28.4 
			</td>
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				Manufacture of metal forming machinery and machine tools 
			</td>
		

			<td valign='top'>
				282 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of metal forming machinery and machine tools, e.g. manufacture of machine tools for working metals and other materials (wood, bone, stone, hard rubber, hard plastics, cold glass etc.), including those using a laser beam, ultrasonic waves, plasma arc, magnetic pulse etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398852
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.41 
			</td>
		

			<td valign='top'>
				28.4 
			</td>
		

			<td valign='top'>
				Manufacture of metal forming machinery 
			</td>
		

			<td valign='top'>
				2822 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of machine tools for working metals, including those using a laser beam, ultrasonic waves, plasma arc, magnetic pulse etc.- manufacture of machine tools for turning, drilling, milling, shaping, planing, boring, grinding etc.- manufacture of stamping or pressing machine tools- manufacture of punch presses, hydraulic presses, hydraulic brakes, drop hammers, forging machines etc.- manufacture of draw-benches, thread rollers or machines for working wires 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of interchangeable tools, see 25.73- manufacture of electrical welding and soldering machines, see 27.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398853
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.49 
			</td>
		

			<td valign='top'>
				28.4 
			</td>
		

			<td valign='top'>
				Manufacture of other machine tools 
			</td>
		

			<td valign='top'>
				2822 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of machine tools for working wood, bone, stone, hard rubber, hard plastics, cold glass etc., including those using a laser beam, ultrasonic waves, plasma arc, magnetic pulse etc.- manufacture of work holders for machine tools- manufacture of dividing heads and other special attachments for machine tools- manufacture of stationary machines for nailing, stapling, glueing or otherwise assembling wood, cork, bone, hard rubber or plastics etc.- manufacture of stationary rotary or rotary percussion drills, filing machines, riveters, sheet metal cutters etc. - manufacture of presses for the manufacture of particle board and the like- manufacture of electroplating machinery 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of parts and accessories for the machine tools listed 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of interchangeable tools for machine tools (drills, punches, dies, taps, milling cutters, turning tools, saw blades, cutting knives etc.), see 25.73- manufacture of electric hand held soldering irons and soldering guns, see 27.90- manufacture of power-driven hand tools, see 28.24- manufacture of machinery used in metal mills or foundries, see 28.91- manufacture of machinery for mining and quarrying, see 28.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398854
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				28 
			</td>
		

			<td valign='top'>
				Manufacture of other special-purpose machinery 
			</td>
		

			<td valign='top'>
				282 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of special-purpose machinery, i.e. machinery for exclusive use in an NACE industry or a small cluster of NACE industries. 
			</td>
		

			<td valign='top'>
				While most of these are used in other manufacturing processes, such as food manufacturing or textile manufacturing, this group also includes the manufacture of machinery specific for other (non-manufacturing industries), such as aircraft launching gear or amusement park equipment. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398855
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.91 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of machinery for metallurgy 
			</td>
		

			<td valign='top'>
				2823 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of machines and equipment for handling hot metals:  • converters, ingot moulds, ladles, casting machines- manufacture of metal-rolling mills and rolls for such mills 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of draw-benches, see 28.41- manufacture of moulding boxes and moulds (except ingot moulds), see 25.73- manufacture of machines for forming foundry moulds, see 28.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398856
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.92 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of machinery for mining, quarrying and construction 
			</td>
		

			<td valign='top'>
				2824 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of continuous-action elevators and conveyors for underground use- manufacture of boring, cutting, sinking and tunnelling machinery (whether or not for underground use)- manufacture of machinery for treating minerals by screening, sorting, separating, washing, crushing etc.- manufacture of concrete and mortar mixers- manufacture of earth-moving machinery:  • bulldozers, angle-dozers, graders, scrapers, levellers, mechanical shovels, shovel loaders etc.- manufacture of pile drivers and pile extractors, mortar spreaders, bitumen spreaders, concrete surfacing machinery etc.- manufacture of track laying tractors and tractors used in construction or mining- manufacture of bulldozer and angle-dozer blades- manufacture of off-road dumping trucks 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of lifting and handling equipment, see 28.22- manufacture of other tractors, see 28.30, 29.10- manufacture of machine tools for working stone, including machines for splitting or clearing stone, see 28.49- manufacture of concrete-mixer lorries, see 29.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398857
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.93 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of machinery for food, beverage and tobacco processing 
			</td>
		

			<td valign='top'>
				2825 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of agricultural dryers- manufacture of machinery for the dairy industry:  • cream separators  • milk processing machinery (e.g. homogenisers)  • milk converting machinery (e.g. butter chums, butter workers and moulding machines)  • cheese-making machines (e.g. homogenisers, moulders, presses) etc.- manufacture of machinery for the grain milling industry:  • machinery to clean, sort or grade seeds, grain or dried leguminous vegetables (winnowers, sieving belts, separators, grain brushing machines etc.)  • machinery to produce flour and meal etc. (grinding mills, feeders, sifters, bran cleaners, blenders, rice hullers, pea splitters)- manufacture of presses, crushers etc. used to make wine, cider, fruit juices etc.- manufacture of machinery for the bakery industry or for making macaroni, spaghetti or similar products:  • bakery ovens, dough mixers, dough-dividers, moulders, slicers, cake depositing machines etc.- manufacture of machines and equipment to process diverse foods:  • machinery to make confectionery, cocoa or chocolate; to manufacture sugar; for breweries; to process meat or poultry, to prepare fruit, nuts or vegetables; to prepare fish, shellfish or other seafood  • machinery for filtering and purifying  • other machinery for the industrial preparation or manufacture of food or drink- manufacture of machinery for the extraction or preparation of animal or vegetable fats or oils- manufacture of machinery for the preparation of tobacco and for the making of cigarettes or cigars, or for pipe or chewing tobacco or snuff- manufacture of machinery for the preparation of food in hotels and restaurants 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of food and milk irradiation equipment, see 26.60- manufacture of packing, wrapping and weighing machinery, see 28.29- manufacture of cleaning, sorting or grading machinery for eggs, fruit or other crops (except seeds, grains and dried leguminous vegetables), see 28.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398858
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.94 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of machinery for textile, apparel and leather production 
			</td>
		

			<td valign='top'>
				2826 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of textile machinery:  • machines for preparing, producing, extruding, drawing, texturing or cutting man-made textile fibres, materials or yarns  • machines for preparing textile fibres: cotton gins, bale breakers, garnetters, cotton spreaders, wool scourers, wool carbonisers, combs, carders, roving frames etc.  • spinning machines  • machines for preparing textile yarns: reelers, warpers and related machines  • weaving machines (looms), including hand looms  • knitting machines  • machines for making knotted net, tulle, lace, braid etc.- manufacture of auxiliary machines or equipment for textile machinery:  • dobbies, jacquards, automatic stop motions, shuttle changing mechanisms, spindles and spindle flyers etc.- manufacture of textile printing machinery- manufacture of machinery for fabric processing:  • machinery for washing, bleaching, dyeing, dressing, finishing, coating or impregnating textile fabrics  • manufacture of machines for reeling, unreeling, folding, cutting or pinking textile fabrics- manufacture of laundry machinery:  • ironing machines, including fusing presses  • commercial washing and drying machines  • dry-cleaning machines- manufacture of sewing machines, sewing machine heads and sewing machine needles (whether or not for household use)- manufacture of machines for producing or finishing felt or non-wovens- manufacture of leather machines:  • machinery for preparing, tanning or working hides, skins or leather  • machinery for making or repairing footwear or other articles of hides, skins, leather or fur skins 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of paper or paperboard cards for use on jacquard machines, see 17.29- manufacture of domestic washing and drying machines, see 27.51- manufacture of calendaring machines, see 28.29- manufacture of machines used in bookbinding, see 28.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398859
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.95 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of machinery for paper and paperboard production 
			</td>
		

			<td valign='top'>
				2829 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of machinery for making paper pulp- manufacture of paper and paperboard making machinery- manufacture of machinery producing articles of paper or paperboard 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398860
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.96 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of plastics and rubber machinery 
			</td>
		

			<td valign='top'>
				2829 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of machinery for working soft rubber or plastics or for the manufacture of products of these materials:  • extruders, moulders, pneumatic tyre making or retreading machines and other machines for making a specific rubber or plastic product 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398861
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				28.99 
			</td>
		

			<td valign='top'>
				28.9 
			</td>
		

			<td valign='top'>
				Manufacture of other special-purpose machinery n.e.c. 
			</td>
		

			<td valign='top'>
				2829 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of special-purpose machinery not elsewhere classified.This class includes:- manufacture of dryers for wood, paper pulp, paper or paperboard and other materials (except for agricultural products and textiles)- manufacture of printing and bookbinding machines and machines for activities supporting printing on a variety of materials - manufacture of machinery for producing tiles, bricks, shaped ceramic pastes, pipes, graphite electrodes, blackboard chalk etc. - manufacture of semi-conductor manufacturing machinery- manufacture of industrial robots performing multiple tasks for special purposes- manufacture of diverse special-purpose machinery and equipment:  • machines to assemble electric or electronic lamps, tubes (valves) or bulbs  • machines for production or hot-working of glass or glassware, glass fibre or yarn  • machinery or apparatus for isotopic separation- manufacture of tyre alignment and balancing equipment; balancing equipment (except wheel balancing) - manufacture of central greasing systems- manufacture of aircraft launching gear, aircraft carrier catapults and related equipment- manufacture of automatic bowling alley equipment (e.g. pin-setters)- manufacture of roundabouts, swings, shooting galleries and other fairground amusements 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of non-metal waste compressing machinery- Manufacture of turnstiles- Manufacture of furling systems, used for winding or releasing the running rigging 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of household appliances, see 27.5- manufacture of photocopy machines etc., see 28.23- manufacture of machinery or equipment to work hard rubber, hard plastics or cold glass, see 28.49- manufacture of ingot moulds, see 28.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398862
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				29 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of motor vehicles, trailers and semi-trailers 
			</td>
		

			<td valign='top'>
				29 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of motor vehicles for transporting passengers or freight. The manufacture of various parts and accessories, as well as the manufacture of trailers and semi-trailers, is included here. The maintenance and repair of vehicles produced in this division are classified in 45.20. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398863
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				29.1 
			</td>
		

			<td valign='top'>
				29 
			</td>
		

			<td valign='top'>
				Manufacture of motor vehicles 
			</td>
		

			<td valign='top'>
				291 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398864
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				29.10 
			</td>
		

			<td valign='top'>
				29.1 
			</td>
		

			<td valign='top'>
				Manufacture of motor vehicles 
			</td>
		

			<td valign='top'>
				2910 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of passenger cars- manufacture of commercial vehicles:  • vans, lorries, over-the-road tractors for semi-trailers etc.- manufacture of buses, trolley-buses and coaches- manufacture of motor vehicle engines- manufacture of chassis for motor vehicles- manufacture of other motor vehicles:  • snowmobiles, golf carts, amphibious vehicles  • fire engines, street sweepers, travelling libraries, armoured cars etc.  • concrete-mixer lorries- ATVs, go-carts and similar including race cars 
			</td>
		

			<td valign='top'>
				This class also includes:- factory rebuilding of motor vehicle engines 
			</td>
		

			<td valign='top'>
				- Manufacture of chassis fitted with engines 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of electric motors (except starting motors), see 27.11- manufacture of lighting equipment for motor vehicles, see 27.40- manufacture of pistons, piston rings and carburettors, see 28.11- manufacture of agricultural tractors, see 28.30- manufacture of tractors used in construction or mining, see 28.92- manufacture of off-road dumping trucks, see 28.92- manufacture of bodies for motor vehicles, see 29.20- manufacture of electrical parts for motor vehicles, see 29.31- manufacture of parts and accessories for motor vehicles, see 29.32- manufacture of tanks and other military fighting vehicles, see 30.40- maintenance and repair of motor vehicles, see 45.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398865
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				29.2 
			</td>
		

			<td valign='top'>
				29 
			</td>
		

			<td valign='top'>
				Manufacture of bodies (coachwork) for motor vehicles; manufacture of trailers and semi-trailers 
			</td>
		

			<td valign='top'>
				292 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398866
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				29.20 
			</td>
		

			<td valign='top'>
				29.2 
			</td>
		

			<td valign='top'>
				Manufacture of bodies (coachwork) for motor vehicles; manufacture of trailers and semi-trailers 
			</td>
		

			<td valign='top'>
				2920 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of bodies, including cabs for motor vehicles- outfitting of all types of motor vehicles, trailers and semi-trailers- manufacture of trailers and semi-trailers:  • tankers, removal trailers etc.  • caravans etc.- manufacture of containers for carriage by one or more modes of transport 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of chassis for trailers 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of trailers and semi-trailers specially designed for use in agriculture, see 28.30- manufacture of parts and accessories of bodies for motor vehicles, see 29.32- manufacture of vehicles drawn by animals, see 30.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398867
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				29.3 
			</td>
		

			<td valign='top'>
				29 
			</td>
		

			<td valign='top'>
				Manufacture of parts and accessories for motor vehicles 
			</td>
		

			<td valign='top'>
				293 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398868
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				29.31 
			</td>
		

			<td valign='top'>
				29.3 
			</td>
		

			<td valign='top'>
				Manufacture of electrical and electronic equipment for motor vehicles 
			</td>
		

			<td valign='top'>
				2930 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of motor vehicle electrical equipment, such as generators, alternators, spark plugs, ignition wiring harnesses, power window and door systems, assembly of purchased gauges into instrument panels, voltage regulators, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of batteries for vehicles, see 27.20- manufacture of lighting equipment for motor vehicles, see 27.40- manufacture of pumps for motor vehicles and engines, see 28.13 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398869
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				29.32 
			</td>
		

			<td valign='top'>
				29.3 
			</td>
		

			<td valign='top'>
				Manufacture of other parts and accessories for motor vehicles 
			</td>
		

			<td valign='top'>
				2930 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of diverse parts and accessories for motor vehicles:  • brakes, gearboxes, axles, road wheels, suspension shock absorbers, radiators, silencers, exhaust pipes, catalytic converters, clutches, steering wheels, steering columns and steering boxes- manufacture of parts and accessories of bodies for motor vehicles:  • safety belts, airbags, doors, bumpers- manufacture of car seats 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of chassis (without engine) for motor vehicles 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of tyres, see 22.11- manufacture of rubber hoses and belts and other rubber products, see 22.19- manufacture of pistons, piston rings and carburettors, see 28.11- maintenance, repair and alteration of motor vehicles, see 45.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398870
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of other transport equipment 
			</td>
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of transportation equipment such as ship building and boat manufacturing, the manufacture of railroad rolling stock and locomotives, air and spacecraft and the manufacture of parts thereof. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398871
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				30.1 
			</td>
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				Building of ships and boats 
			</td>
		

			<td valign='top'>
				301 
			</td>
		

			<td valign='top'>
				This group includes the building of ships, boats and other floating structures for transportation and other commercial purposes, as well as for sports and recreational purposes. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398872
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.11 
			</td>
		

			<td valign='top'>
				30.1 
			</td>
		

			<td valign='top'>
				Building of ships and floating structures 
			</td>
		

			<td valign='top'>
				3011 
			</td>
		

			<td valign='top'>
				This class includes the building of ships, except vessels for sports or recreation, and the construction of floating structures:This class includes:- building of commercial vessels:   • passenger vessels, ferry boats, cargo ships, tankers, tugs etc.- building of warships- building of fishing boats and fish-processing factory vessels 
			</td>
		

			<td valign='top'>
				This class also includes:- building of hovercraft (except recreation-type hovercraft)- construction of drilling platforms, floating or submersible- construction of floating structures:  • floating docks, pontoons, coffer-dams, floating landing stages, buoys, floating tanks, barges, lighters, floating cranes, non-recreational inflatable rafts etc.- manufacture of sections for ships and floating structures 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of parts of vessels, other than major hull assemblies:  • manufacture of sails, see 13.92  • manufacture of ships' propellers, see 25.99  • manufacture of iron or steel anchors, see 25.99  • manufacture of marine engines, see 28.11- manufacture of navigational instruments, see 26.51- manufacture of lighting equipment for ships, see 27.40- manufacture of amphibious motor vehicles, see 29.10- manufacture of inflatable boats or rafts for recreation, see 30.12- specialised repair and maintenance of ships and floating structures, see 33.15- ship-breaking, see 38.31- interior installation of boats, see 43.3 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398873
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.12 
			</td>
		

			<td valign='top'>
				30.1 
			</td>
		

			<td valign='top'>
				Building of pleasure and sporting boats 
			</td>
		

			<td valign='top'>
				3012 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of inflatable boats and rafts- building of sailboats with or without auxiliary motor- building of motor boats- building of recreation-type hovercraft- manufacture of personal watercraft- manufacture of other pleasure and sporting boats:  • canoes, kayaks, rowing boats, skiffs 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of parts of pleasure and sporting boats:  • manufacture of sails, see 13.92  • manufacture of iron or steel anchors, see 25.99  • manufacture of marine engines, see 28.11- manufacture of sailboards and surfboards, see 32.30- maintenance and repair of pleasure boats, see 33.15 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398874
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				30.2 
			</td>
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				Manufacture of railway locomotives and rolling stock 
			</td>
		

			<td valign='top'>
				302 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398875
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.20 
			</td>
		

			<td valign='top'>
				30.2 
			</td>
		

			<td valign='top'>
				Manufacture of railway locomotives and rolling stock 
			</td>
		

			<td valign='top'>
				3020 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of electric, diesel, steam and other rail locomotives- manufacture of self-propelled railway or tramway coaches, vans and trucks, maintenance or service vehicles- manufacture of railway or tramway rolling stock, not self-propelled:  • passenger coaches, goods vans, tank wagons, self-discharging vans and wagons, workshop vans, crane vans, tenders etc.- manufacture of specialised parts of railway or tramway locomotives or of rolling stock:  • bogies, axles and wheels, brakes and parts of brakes; hooks and coupling devices, buffers and buffer parts; shock absorbers; wagon and locomotive frames; bodies; corridor connections etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of mining locomotives and mining rail cars- manufacture of mechanical and electromechanical signalling, safety and traffic control equipment for railways, tramways, inland waterways, roads, parking facilities, airfields etc.- manufacture of railway car seats 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of unassembled rails, see 24.10- manufacture of assembled railway track fixtures, see 25.99- manufacture of electric motors, see 27.11- manufacture of electrical signalling, safety or traffic-control equipment, see 27.90- manufacture of engines and turbines, see 28.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398876
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				30.3 
			</td>
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				Manufacture of air and spacecraft and related machinery 
			</td>
		

			<td valign='top'>
				303 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398877
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.30 
			</td>
		

			<td valign='top'>
				30.3 
			</td>
		

			<td valign='top'>
				Manufacture of air and spacecraft and related machinery 
			</td>
		

			<td valign='top'>
				3030 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of airplanes for the transport of goods or passengers, for use by the defence forces, for sport or other purposes- manufacture of helicopters- manufacture of gliders, hang-gliders- manufacture of dirigibles and hot air balloons- manufacture of parts and accessories of the aircraft of this class:  • major assemblies such as fuselages, wings, doors, control surfaces, landing gear, fuel tanks, nacelles etc.  • airscrews, helicopter rotors and propelled rotor blades  • motors and engines of a kind typically found on aircraft  • parts of turbojets and turboprops for aircraft- manufacture of ground flying trainers- manufacture of spacecraft and launch vehicles, satellites, planetary probes, orbital stations, shuttles- manufacture of intercontinental ballistic missiles (ICBM) 
			</td>
		

			<td valign='top'>
				This class also includes:- overhaul and conversion of aircraft or aircraft engines- manufacture of aircraft seats 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of parachutes, see 13.92- manufacture of military ordinance and ammunition, see 25.40- manufacture of telecommunication equipment for satellites, see 26.30- manufacture of aircraft instrumentation and aeronautical instruments, see 26.51- manufacture of air navigation systems, see 26.51- manufacture of lighting equipment for aircraft, see 27.40- manufacture of ignition parts and other electrical parts for internal combustion engines, see 27.90- manufacture of pistons, piston rings and carburettors, see 28.11- manufacture of aircraft launching gear, aircraft carrier catapults and related equipment, see 28.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398878
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				30.4 
			</td>
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				Manufacture of military fighting vehicles 
			</td>
		

			<td valign='top'>
				304 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398879
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.40 
			</td>
		

			<td valign='top'>
				30.4 
			</td>
		

			<td valign='top'>
				Manufacture of military fighting vehicles 
			</td>
		

			<td valign='top'>
				3040 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of tanks- manufacture of armoured amphibious military vehicles- manufacture of other military fighting vehicles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of weapons and ammunitions, see 25.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398880
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				30.9 
			</td>
		

			<td valign='top'>
				30 
			</td>
		

			<td valign='top'>
				Manufacture of transport equipment n.e.c. 
			</td>
		

			<td valign='top'>
				309 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of transport equipment other than motor vehicles and rail, water, air or space transport equipment and military vehicles. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398881
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.91 
			</td>
		

			<td valign='top'>
				30.9 
			</td>
		

			<td valign='top'>
				Manufacture of motorcycles 
			</td>
		

			<td valign='top'>
				3091 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of motorcycles, mopeds and cycles fitted with an auxiliary engine- manufacture of engines for motorcycles- manufacture of sidecars- manufacture of parts and accessories for motorcycles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of bicycles, see 30.92- manufacture of invalid carriages, see 30.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398882
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.92 
			</td>
		

			<td valign='top'>
				30.9 
			</td>
		

			<td valign='top'>
				Manufacture of bicycles and invalid carriages 
			</td>
		

			<td valign='top'>
				3092 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of non-motorised bicycles and other cycles, including (delivery) tricycles, tandems, children's bicycles and tricycles- manufacture of parts and accessories of bicycles- manufacture of invalid carriages with or without motor- manufacture of parts and accessories of invalid carriages- manufacture of baby carriages 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of bicycles with auxiliary motor, see 30.91- manufacture of wheeled toys designed to be ridden, including plastic bicycles and tricycles, see 32.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398883
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				30.99 
			</td>
		

			<td valign='top'>
				30.9 
			</td>
		

			<td valign='top'>
				Manufacture of other transport equipment n.e.c. 
			</td>
		

			<td valign='top'>
				3099 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of hand-propelled vehicles: luggage trucks, handcarts, sledges, shopping carts etc.- manufacture of vehicles drawn by animals: sulkies, donkey-carts, hearses etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- works trucks, whether or not fitted with lifting or handling equipment, whether or not self-propelled, of the type used in factories (including hand trucks and wheelbarrows), see 28.22- decorative restaurant carts, such as a desert cart, food wagons, see 31.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398884
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				31 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Manufacture of furniture 
			</td>
		

			<td valign='top'>
				31 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of furniture and related products of any material except stone, concrete and ceramic. The processes used in the manufacture of furniture are standard methods of forming materials and assembling components, including cutting, moulding and laminating. The design of the article, for both aesthetic and functional qualities, is an important aspect of the production process.Some of the processes used in furniture manufacturing are similar to processes that are used in other segments of manufacturing. For example, cutting and assembly occurs in the production of wood trusses that are classified in division 16 (Manufacture of wood and wood products). However, the multiple processes distinguish wood furniture manufacturing from wood product manufacturing. Similarly, metal furniture manufacturing uses techniques that are also employed in the manufacture of roll-formed products classified in division 25 (Manufacture of fabricated metal products). The moulding process for plastics furniture is similar to the moulding of other plastics products. However, the manufacture of plastics furniture tends to be a specialised activity. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398885
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				31.0 
			</td>
		

			<td valign='top'>
				31 
			</td>
		

			<td valign='top'>
				Manufacture of furniture 
			</td>
		

			<td valign='top'>
				310 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398886
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				31.01 
			</td>
		

			<td valign='top'>
				31.0 
			</td>
		

			<td valign='top'>
				Manufacture of office and shop furniture 
			</td>
		

			<td valign='top'>
				3100 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of furniture of any kind, any material (except stone, concrete or ceramic) for any place and various purposes.This class includes:- manufacture of chairs and seats for offices, workrooms, hotels, restaurants and public premises- manufacture of chairs and seats for theatres, cinemas and the like- manufacture of special furniture for shops: counters, display cases, shelves etc.- manufacture of office furniture- manufacture of laboratory benches, stools, and other laboratory seating, laboratory furniture (e.g. cabinets and tables)- manufacture of furniture for churches, schools, restaurants 
			</td>
		

			<td valign='top'>
				This class also includes:- decorative restaurant carts, such as a desert cart, food wagons 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- blackboards, see 28.23- manufacture of car seats, see 29.32- manufacture of railway car seats, see 30.20- manufacture of aircraft seats, see 30.30- manufacture of medical, surgical, dental or veterinary furniture, see 32.50- modular furniture attachment and installation, partition installation, laboratory equipment furniture installation, see 43.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398887
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				31.02 
			</td>
		

			<td valign='top'>
				31.0 
			</td>
		

			<td valign='top'>
				Manufacture of kitchen furniture 
			</td>
		

			<td valign='top'>
				3100 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of kitchen furniture 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398888
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				31.03 
			</td>
		

			<td valign='top'>
				31.0 
			</td>
		

			<td valign='top'>
				Manufacture of mattresses 
			</td>
		

			<td valign='top'>
				3100 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of mattresses:  • mattresses fitted with springs or stuffed or internally fitted with a supporting material  • uncovered cellular rubber or plastic mattresses- manufacture of mattress supports 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of inflatable rubber mattresses, see 22.19- manufacture of rubber waterbed mattresses, see 22.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398889
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				31.09 
			</td>
		

			<td valign='top'>
				31.0 
			</td>
		

			<td valign='top'>
				Manufacture of other furniture 
			</td>
		

			<td valign='top'>
				3100 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of sofas, sofa beds and sofa sets- manufacture of garden chairs and seats- manufacture of furniture for bedrooms, living rooms, gardens etc.- manufacture of cabinets for sewing machines, televisions etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- finishing such as upholstery of chairs and seats- finishing of furniture such as spraying, painting, French polishing and upholstering 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of pillows, pouffes, cushions, quilts and eiderdowns, see 13.92- manufacture of furniture of ceramics, concrete and stone, see 23.42, 23.69, 23.70- manufacture of lighting fittings or lamps, see 27.40- manufacture of car seats, see 29.32- manufacture of railway car seats, see 30.20- manufacture of aircraft seats, see 30.30- reupholstering and restoring of furniture, see 95.24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398890
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Other manufacturing 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				This division includes the manufacture of a variety of goods not covered in other parts of the classification. Since this is a residual division, production processes, input materials and use of the produced goods can vary widely and usual criteria for grouping classes into divisions have not been applied here. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398891
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				32.1 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				Manufacture of jewellery, bijouterie and related articles 
			</td>
		

			<td valign='top'>
				321 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of jewellery and imitation jewellery articles. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398892
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.11 
			</td>
		

			<td valign='top'>
				32.1 
			</td>
		

			<td valign='top'>
				Striking of coins 
			</td>
		

			<td valign='top'>
				3211 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of coins, including coins for use as legal tender, whether or not of precious metal 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398893
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.12 
			</td>
		

			<td valign='top'>
				32.1 
			</td>
		

			<td valign='top'>
				Manufacture of jewellery and related articles 
			</td>
		

			<td valign='top'>
				3211 
			</td>
		

			<td valign='top'>
				This class includes:- production of worked pearls- production of precious and semi-precious stones in the worked state, including the working of industrial quality stones and synthetic or reconstructed precious or semi-precious stones- working of diamonds- manufacture of jewellery of precious metal or of base metals clad with precious metals, or precious or semi-precious stones, or of combinations of precious metal and precious or semi-precious stones or of other materials- manufacture of goldsmiths' articles of precious metals or of base metals clad with precious metals:  • dinnerware, flatware, hollowware, toilet articles, office or desk articles, articles for religious use etc.- manufacture of technical or laboratory articles of precious metal (except instruments and parts thereof): crucibles, spatulas, electroplating anodes etc.- manufacture of precious metal watch bands, wristbands, watch straps and cigarette cases 
			</td>
		

			<td valign='top'>
				This class also includes:- engraving of personal precious and non-precious metal products 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of non-metal watch bands (fabric, leather, plastic etc.), see 15.12- manufacture of articles of base metal plated with precious metal (except imitation jewellery), see division 25- manufacture of watchcases, see 26.52- manufacture of (non-precious) metal watch bands, see 32.13- manufacture of imitation jewellery, see 32.13- repair of jewellery, see 95.25 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398894
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.13 
			</td>
		

			<td valign='top'>
				32.1 
			</td>
		

			<td valign='top'>
				Manufacture of imitation jewellery and related articles 
			</td>
		

			<td valign='top'>
				3212 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of costume or imitation jewellery:  • rings, bracelets, necklaces, and similar articles of jewellery made from base metals plated with precious metals  • jewellery containing imitation stones such as imitation gems stones, imitation diamonds, and similar- manufacture of metal watch bands (except precious metal) 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of jewellery made from precious metals or clad with precious metals, see 32.12- manufacture of jewellery containing genuine gem stones, see 32.12- manufacture of precious metal watch bands, see 32.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398895
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				32.2 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				Manufacture of musical instruments 
			</td>
		

			<td valign='top'>
				322 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398896
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.20 
			</td>
		

			<td valign='top'>
				32.2 
			</td>
		

			<td valign='top'>
				Manufacture of musical instruments 
			</td>
		

			<td valign='top'>
				3220 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of stringed instruments- manufacture of keyboard stringed instruments, including automatic pianos- manufacture of keyboard pipe organs, including harmoniums and similar keyboard instruments with free metal reeds- manufacture of accordions and similar instruments, including mouth organs- manufacture of wind instruments- manufacture of percussion musical instruments- manufacture of musical instruments, the sound of which is produced electronically- manufacture of musical boxes, fairground organs, calliopes etc.- manufacture of instrument parts and accessories:  • metronomes, tuning forks, pitch pipes, cards, discs and rolls for automatic mechanical instruments etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- manufacture of whistles, call horns and other mouth-blown sound signalling instruments 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- reproduction of pre-recorded sound and video tapes and discs, see 18.2- manufacture of microphones, amplifiers, loudspeakers, headphones and similar components, see 26.40- manufacture of record players, tape recorders and the like, see 26.40- manufacture of toy musical instruments, see 32.40- restoring of organs and other historic musical instruments, see 33.19- publishing of pre-recorded sound and video tapes and discs, see 59.20- piano tuning, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398897
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				32.3 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				Manufacture of sports goods 
			</td>
		

			<td valign='top'>
				323 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398898
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.30 
			</td>
		

			<td valign='top'>
				32.3 
			</td>
		

			<td valign='top'>
				Manufacture of sports goods 
			</td>
		

			<td valign='top'>
				3230 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of sporting and athletic goods (except apparel and footwear).This class includes:- manufacture of articles and equipment for sports, outdoor and indoor games, of any material:  • hard, soft and inflatable balls  • rackets, bats and clubs  • skis, bindings and poles  • ski-boots  • sailboards and surfboards  • requisites for sport fishing, including landing nets  • requisites for hunting, mountain climbing etc.  • leather sports gloves and sports headgear  • basins for swimming and padding pools etc.  • ice skates, roller skates etc.  • bows and crossbows  • gymnasium, fitness centre or athletic equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of boat sails, see 13.92- manufacture of sports apparel, see 14.19- manufacture of saddlery and harness, see 15.12- manufacture of whips and riding crops, see 15.12- manufacture of sports footwear, see 15.20- manufacture of sporting weapons and ammunition, see 25.40- manufacture of metal weights as used for weightlifting, see 25.99- manufacture of sports vehicles other than toboggans and the like, see divisions 29 and 30- manufacture of boats, see 30.12- manufacture of billiard tables, see 32.40- manufacture of ear and noise plugs (e.g. for swimming and noise protection), see 32.99- repair of sporting goods, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398899
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				32.4 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				Manufacture of games and toys 
			</td>
		

			<td valign='top'>
				324 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398900
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.40 
			</td>
		

			<td valign='top'>
				32.4 
			</td>
		

			<td valign='top'>
				Manufacture of games and toys 
			</td>
		

			<td valign='top'>
				3240 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of dolls, toys and games (including electronic games), scale models and children's vehicles (except metal bicycles and tricycles).This class includes:- manufacture of dolls and doll garments, parts and accessories- manufacture of action figures- manufacture of toy animals- manufacture of toy musical instruments- manufacture of playing cards- manufacture of board games and similar games- manufacture of electronic games: chess etc.- manufacture of reduced-size (&quot;scale&quot;) models and similar recreational models, electrical trains, construction sets etc.- manufacture of coin-operated games, billiards, special tables for casino games, etc.- manufacture of articles for funfair, table or parlour games- manufacture of wheeled toys designed to be ridden, including plastic bicycles and tricycles- manufacture of puzzles and similar articles 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of toys made of fabric 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of video game consoles, see 26.40- manufacture of bicycles, see 30.92- manufacture of articles for jokes and novelties, see 32.99- writing and publishing of software for video game consoles, see 58.21, 62.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398901
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				32.5 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				Manufacture of medical and dental instruments and supplies 
			</td>
		

			<td valign='top'>
				325 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398902
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.50 
			</td>
		

			<td valign='top'>
				32.5 
			</td>
		

			<td valign='top'>
				Manufacture of medical and dental instruments and supplies 
			</td>
		

			<td valign='top'>
				3250 
			</td>
		

			<td valign='top'>
				This class includes the manufacture of laboratory apparatus, surgical and medical instruments, surgical appliances and supplies, dental equipment and supplies, orthodontic goods, dentures and orthodontic appliances. Included is the manufacture of medical, dental and similar furniture, where the additional specific functions determine the purpose of the product, such as dentist's chairs with built-in hydraulic functions.This class includes:- manufacture of surgical drapes and sterile string and tissue- manufacture of dental fillings and cements (except denture adhesives), dental wax and other dental plaster preparations- manufacture of bone reconstruction cements- manufacture of dental laboratory furnaces- manufacture of laboratory ultrasonic cleaning machinery- manufacture of laboratory sterilisers- manufacture of laboratory type distilling apparatus, laboratory centrifuges- manufacture of medical, surgical, dental or veterinary furniture, such as:  • operating tables  • examination tables  • hospital beds with mechanical fittings  • dentists' chairs- manufacture of bone plates and screws, syringes, needles, catheters, cannulae, etc.- manufacture of dental instruments (including dentists' chairs incorporating dental equipment)- manufacture of artificial teeth, bridges, etc., made in dental labs- manufacture of orthopedic and prosthetic devices- manufacture of glass eyes- manufacture of medical thermometers- manufacture of ophthalmic goods, eyeglasses, sunglasses, lenses ground to prescription, contact lenses, safety goggles 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of massage chairs 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of denture adhesives, see 20.42- manufacture of medical impregnated wadding, dressings etc., see 21.20- manufacture of electromedical and electrotherapeutic equipment, see 26.60- manufacture of wheelchairs, see 30.92- activities of opticians, see 47.78 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398903
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				32.9 
			</td>
		

			<td valign='top'>
				32 
			</td>
		

			<td valign='top'>
				Manufacturing n.e.c. 
			</td>
		

			<td valign='top'>
				329 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398904
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.91 
			</td>
		

			<td valign='top'>
				32.9 
			</td>
		

			<td valign='top'>
				Manufacture of brooms and brushes 
			</td>
		

			<td valign='top'>
				3290 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of brooms and brushes, including brushes constituting parts of machines, hand-operated mechanical floor sweepers, mops and feather dusters, paint brushes, paint pads and rollers, squeegees and other brushes, brooms, mops etc.- manufacture of shoe and clothes brushes 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398905
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				32.99 
			</td>
		

			<td valign='top'>
				32.9 
			</td>
		

			<td valign='top'>
				Other manufacturing n.e.c. 
			</td>
		

			<td valign='top'>
				3290 
			</td>
		

			<td valign='top'>
				This class includes:- manufacture of protective safety equipment  • manufacture of fire resistant and protective safety clothing  • manufacture of linemen's safety belts and other belts for occupational use  • manufacture of cork life preservers  • manufacture of plastics hard hats and other personal safety equipment of plastics (e.g. athletic helmets)  • manufacture of fire fighting protection suits  • manufacture of metal safety headgear and other metal personal safety devices  • manufacture of ear and noise plugs (e.g. for swimming and noise protection)  • manufacture of gas masks- manufacture of pens and pencils of all kinds whether or not mechanical- manufacture of pencil leads- manufacture of date, sealing or numbering stamps, hand-operated devices for printing, or embossing labels, hand printing sets, prepared typewriter ribbons and inked pads- manufacture of globes- manufacture of umbrellas, sun-umbrellas, walking sticks, seat-sticks- manufacture of buttons, press-fasteners, snap-fasteners, press-studs, slide fasteners- manufacture of cigarette lighters- manufacture of articles of personal use: smoking pipes, scent sprays, vacuum flasks and other vacuum vessels for personal or household use, wigs, false beards, eyebrows- manufacture of miscellaneous articles: candles, tapers and the like; artificial flowers, fruit and foliage; jokes and novelties; hand sieves and hand riddles; tailors' dummies; burial coffins etc.- manufacture of floral baskets, bouquets, wreaths and similar articles- taxidermy activities 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Manufacture of scale models 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of lighter wicks, see 13.96- manufacture of workwear and service apparel (e.g. laboratory coats, work overalls, uniforms), see 14.12- manufacture of paper novelties, see 17.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398906
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				33 
			</td>
		

			<td valign='top'>
				C 
			</td>
		

			<td valign='top'>
				Repair and installation of machinery and equipment 
			</td>
		

			<td valign='top'>
				33 
			</td>
		

			<td valign='top'>
				This division includes the specialised repair of goods produced in the manufacturing sector with the aim to restore machinery, equipment and other products to working order. The provision of general or routine maintenance (i.e. servicing) on such products to ensure they work efficiently and to prevent breakdown and unnecessary repairs is included. This division does only include specialised repair and maintenance activities. A substantial amount of repair is also done by manufacturers of machinery, equipment and other goods, in which case the classification of units engaged in these repair and manufacturing activities is done according to the value-added principle which would often assign these combined activities to the manufacture of the good. The same principle is applied for combined trade and repair. The rebuilding or remanufacture of machinery and equipment is considered a manufacturing activity and included in other divisions of this section.Repair and maintenance of goods that are utilised as capital goods as well as consumer goods is typically classified as repair and maintenance of household goods (e.g. office and household furniture repair, see 95.24). 
			</td>
		

			<td valign='top'>
				Also included in this division is the specialised installation of machinery. However, the installation of equipment that forms an integral part of buildings or similar structures, such as installation of electrical wiring, installation of escalators or installation of air-conditioning systems, is classified as construction. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes:- cleaning of industrial machinery, see 81.22- repair and maintenance of computers and communications equipment, see 95.1- repair and maintenance of household goods, see 95.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398907
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				33 
			</td>
		

			<td valign='top'>
				Repair of fabricated metal products, machinery and equipment 
			</td>
		

			<td valign='top'>
				331 
			</td>
		

			<td valign='top'>
				This group includes the specialised repair of goods produced in the manufacturing sector with the aim to restore these metal products, machinery, equipment and other products to working order. The provision of general or routine maintenance (i.e. servicing) on such products to ensure they work efficiently and to prevent breakdown and unnecessary repairs is included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- rebuilding or remanufacture of machinery and equipment, see corresponding class in divisions 25-30- cleaning of industrial machinery, see 81.22- repair and maintenance of computers and communications equipment, see 95.1- repair and maintenance of household goods, see 95.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398908
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.11 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair of fabricated metal products 
			</td>
		

			<td valign='top'>
				3311 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of fabricated metal products of division 25.This class includes:- repair of metal tanks, reservoirs and containers- repair and maintenance for pipes and pipelines- mobile welding repair- repair of steel shipping drums- repair and maintenance of steam or other vapour generators- repair and maintenance of auxiliary plant for use with steam generators:  • condensers, economisers, superheaters, steam collectors and accumulators- repair and maintenance of nuclear reactors, except isotope separators- repair and maintenance of parts for marine or power boilers- platework repair of central heating boilers and radiators- repair and maintenance of fire arms and ordnance (including repair of sporting and recreational guns)- repair and maintenance of shopping carts 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- sharpening of blades and saws, see 33.12- repair of central heating systems etc., see 43.22- repair of mechanical locking devices, safes etc., see 80.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398909
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.12 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair of machinery 
			</td>
		

			<td valign='top'>
				3312 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of industrial machinery and equipment like sharpening or installing commercial and industrial machinery blades and saws; the provision of welding (e.g. automotive, general) repair services; the repair of agricultural and other heavy and industrial machinery and equipment (e.g. forklifts and other materials handling equipment, machine tools, commercial refrigeration equipment, construction equipment and mining machinery), including machinery and equipment of division 28.This class includes: - repair and maintenance of non-motor vehicle engines- repair and maintenance of pumps, compressors and related equipment- repair and maintenance of fluid power machinery- repair of valves- repair of gearing and driving elements- repair and maintenance of industrial process furnaces- repair and maintenance of lifting and handling equipment- repair and maintenance of industrial refrigeration equipment and air purifying equipment- repair and maintenance of commercial-type general-purpose machinery- repair of power-driven hand-tools- repair and maintenance of metal cutting and metal forming machine tools and accessories- repair and maintenance of other machine tools- repair and maintenance of agricultural tractors- repair and maintenance of agricultural machinery and forestry and logging machinery- repair and maintenance of metallurgy machinery- repair and maintenance of mining, construction, and oil and gas field machinery- repair and maintenance of food, beverage, and tobacco processing machinery- repair and maintenance of textile apparel, and leather production machinery- repair and maintenance of papermaking machinery- repair and maintenance of plastic and rubber machinery- repair and maintenance of other special-purpose machinery of division 28- repair and maintenance of weighing equipment - repair and maintenance of vending machines- repair and maintenance of cash registers- repair and maintenance of photocopy machines- repair of calculators, electronic or not- repair of typewriters 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes: - installation, repair and maintenance of furnaces and other heating equipment, see 43.22- installation, repair and maintenance of elevators and escalators, see 43.29- repair of computers, see 95.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398910
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.13 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair of electronic and optical equipment 
			</td>
		

			<td valign='top'>
				3313 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of goods produced in groups 26.5, 26.6 and 26.7, except those that are considered household goods.This class includes:- repair and maintenance of the measuring, testing, navigating and control equipment of group 26.5, such as:   • aircraft engine instruments  • automotive emissions testing equipment  • meteorological instruments  • physical, electrical and chemical properties testing and inspection equipment  • surveying instruments  • radiation detection and monitoring instruments- repair and maintenance of irradiation, electromedical and electrotherapeutic equipment of class 26.60, such as:   • magnetic resonance imaging equipment  • medical ultrasound equipment  • pacemakers  • hearing aids  • electrocardiographs  • electromedical endoscopic equipment   • irradiation apparatus- repair and maintenance of optical instruments and equipment of class 26.70, if the use is mainly commercial, such as:  • binoculars  • microscopes (except electron and proton microscopes)  • telescopes  • prisms and lenses (except ophthalmic)  • photographic equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- repair and maintenance of photocopy machines, see 33.12- repair and maintenance of computers and peripheral equipment, see 95.11- repair and maintenance of computer projectors, see 95.11- repair and maintenance of communication equipment, see 95.12- repair and maintenance of commercial TV and video cameras, see 95.12- repair of household-type video cameras, see 95.21- repair of watches and clocks, see 95.25 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398911
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.14 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair of electrical equipment 
			</td>
		

			<td valign='top'>
				3314 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of goods of division 27, except those in class group 27.5 (domestic appliances). This class includes:- repair and maintenance of power, distribution, and specialty transformers- repair and maintenance of electric motors, generators, and motor generator sets- repair and maintenance of switchgear and switchboard apparatus- repair and maintenance of relays and industrial controls- repair and maintenance of primary and storage batteries- repair and maintenance of electric lighting equipment- repair and maintenance of current-carrying wiring devices and non current-carrying wiring devices for wiring electrical circuits 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- repair and maintenance of computers and peripheral computer equipment, see 95.11- repair and maintenance of telecommunications equipment, see 95.12- repair and maintenance of consumer electronics, see 95.21- repair of watches and clocks, see 95.25 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398912
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.15 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair and maintenance of ships and boats 
			</td>
		

			<td valign='top'>
				3315 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of ships and boats. However, the factory rebuilding or overhaul of ships is classified in division 30.This class includes:- repair and routine maintenance of ships- repair and maintenance of pleasure boats 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Repair and maintenance of oil rigs 
			</td>
		

			<td valign='top'>
				This class excludes:- factory conversion of ships, see 30.1- repair of ship and boat engines, see 33.12- ship scrapping, dismantling, see 38.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398913
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.16 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair and maintenance of aircraft and spacecraft 
			</td>
		

			<td valign='top'>
				3315 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of aircraft and spacecraft. This class includes:- repair and maintenance of aircraft (except factory conversion, factory overhaul, factory rebuilding)- repair and maintenance of aircraft engines 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- factory overhaul and rebuilding of aircraft, see 30.30- factory overhaul of aircraft engines, see 30.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398914
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.17 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair and maintenance of other transport equipment 
			</td>
		

			<td valign='top'>
				3315 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of other transport equipment of division 30, except motorcycles and bicycles. This class includes:- repair and maintenance of locomotives and railroad cars (except factory rebuilding or factory conversion)- repair of animal drawn buggies and wagons 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- factory overhaul and rebuilding of locomotives and railroad cars, see 30.20- repair and maintenance of military fighting vehicles, see 30.40- repair and maintenance of shopping carts, see 33.11- repair and maintenance of railway engines, see 33.12- repair and maintenance of motorcycles, see 45.40- repair of bicycles, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398915
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.19 
			</td>
		

			<td valign='top'>
				33.1 
			</td>
		

			<td valign='top'>
				Repair of other equipment 
			</td>
		

			<td valign='top'>
				3319 
			</td>
		

			<td valign='top'>
				This class includes the repair and maintenance of equipment not covered in other groups of this division.This class includes:- repair of fishing nets, including mending- repair or ropes, riggings, canvas and tarpaulins- repair of fertiliser and chemical storage bags- repair or reconditioning of wooden pallets, shipping drums or barrels, and similar items- repair of pinball machines and other coin-operated games- restoring of organs and other historical musical instruments 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Repair of hospital beds 
			</td>
		

			<td valign='top'>
				This class excludes:- repair of household and office type furniture, furniture restoration, see 95.24- repair of bicycles, see 95.29- repair and alteration of clothing, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398916
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				33.2 
			</td>
		

			<td valign='top'>
				33 
			</td>
		

			<td valign='top'>
				Installation of industrial machinery and equipment 
			</td>
		

			<td valign='top'>
				332 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398917
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				33.20 
			</td>
		

			<td valign='top'>
				33.2 
			</td>
		

			<td valign='top'>
				Installation of industrial machinery and equipment 
			</td>
		

			<td valign='top'>
				3320 
			</td>
		

			<td valign='top'>
				This class includes the specialised installation of machinery. However, the installation of equipment that forms an integral part of buildings or similar structures, such as installation of escalators, electrical wiring, burglar alarm systems or air-conditioning systems, is classified as construction.This class includes:- installation of industrial machinery in industrial plant- installation and assembly of industrial process control equipment- installation of other industrial equipment, e.g.:  • communications equipment  • mainframe and similar computers  • irradiation and electromedical equipment etc.- dismantling large-scale machinery and equipment- activities of millwrights- machine rigging- installation of bowling alley equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of elevators, escalators, automated doors, vacuum cleaning systems etc., see 43.29- installation of doors, staircases, shop fittings, furniture etc., see 43.32- installation (setting-up) of personal computers, see 62.09 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398918
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				D 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				ELECTRICITY, GAS, STEAM AND AIR CONDITIONING SUPPLY 
			</td>
		

			<td valign='top'>
				D 
			</td>
		

			<td valign='top'>
				This section includes the activity of providing electric power, natural gas, steam, hot water and the like through a permanent infrastructure (network) of lines, mains and pipes. The dimension of the network is not decisive; also included are the distribution of electricity, gas, steam, hot water and the like in industrial parks or residential buildings.This section therefore includes the operation of electric and gas utilities, which generate, control and distribute electric power or gas. 
			</td>
		

			<td valign='top'>
				Also included is the provision of steam and air-conditioning supply. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This section excludes the operation of water and sewerage utilities, see 36, 37. This section also excludes the (typically long-distance) transport of gas through pipelines. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398919
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				35 
			</td>
		

			<td valign='top'>
				D 
			</td>
		

			<td valign='top'>
				Electricity, gas, steam and air conditioning supply 
			</td>
		

			<td valign='top'>
				35 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398920
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				35.1 
			</td>
		

			<td valign='top'>
				35 
			</td>
		

			<td valign='top'>
				Electric power generation, transmission and distribution 
			</td>
		

			<td valign='top'>
				351 
			</td>
		

			<td valign='top'>
				This group includes the generation of bulk electric power, transmission from generating facilities to distribution centres and distribution to end users. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398921
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.11 
			</td>
		

			<td valign='top'>
				35.1 
			</td>
		

			<td valign='top'>
				Production of electricity 
			</td>
		

			<td valign='top'>
				3510 
			</td>
		

			<td valign='top'>
				This class includes:- operation of generation facilities that produce electric energy; including thermal, nuclear, hydroelectric, gas turbine, diesel and renewable 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of electricity through incineration of waste, see 38.21 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398922
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.12 
			</td>
		

			<td valign='top'>
				35.1 
			</td>
		

			<td valign='top'>
				Transmission of electricity 
			</td>
		

			<td valign='top'>
				3510 
			</td>
		

			<td valign='top'>
				This class includes:- operation of transmission systems that convey the electricity from the generation facility to the distribution system 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398923
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.13 
			</td>
		

			<td valign='top'>
				35.1 
			</td>
		

			<td valign='top'>
				Distribution of electricity 
			</td>
		

			<td valign='top'>
				3510 
			</td>
		

			<td valign='top'>
				This class includes:- operation of distribution systems (i.e., consisting of lines, poles, meters, and wiring) that convey electric power received from the generation facility or the transmission system to the final consumer 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398924
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.14 
			</td>
		

			<td valign='top'>
				35.1 
			</td>
		

			<td valign='top'>
				Trade of electricity 
			</td>
		

			<td valign='top'>
				3510 
			</td>
		

			<td valign='top'>
				This class includes:- sale of electricity to the user- activities of electric power brokers or agents that arrange the sale of electricity via power distribution systems operated by others- operation of electricity and transmission capacity exchanges for electric power 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Charging stations for mobile phones and laptops 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398925
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				35.2 
			</td>
		

			<td valign='top'>
				35 
			</td>
		

			<td valign='top'>
				Manufacture of gas; distribution of gaseous fuels through mains 
			</td>
		

			<td valign='top'>
				352 
			</td>
		

			<td valign='top'>
				This group includes the manufacture of gas and the distribution of natural or synthetic gas to the consumer through a system of mains. Gas marketers or brokers, which arrange the sale of natural gas over distribution systems operated by others, are included. The separate operation of gas pipelines, typically done over long distances, connecting producers with distributors of gas, or between urban centres, is excluded from this group and classified with other pipeline transport activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398926
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.21 
			</td>
		

			<td valign='top'>
				35.2 
			</td>
		

			<td valign='top'>
				Manufacture of gas 
			</td>
		

			<td valign='top'>
				3520 
			</td>
		

			<td valign='top'>
				This class includes:- production of gas for the purpose of gas supply by carbonation of coal, from by-products of agriculture or from waste- manufacture of gaseous fuels with a specified calorific value, by purification, blending and other processes from gases of various types including natural gas 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Gasification of lignite- Transformation of pipeline natural gas into compressed natural gas (CNG) off mine site 
			</td>
		

			<td valign='top'>
				This class excludes:- production of crude natural gas, see 06.20- operation of coke ovens, see 19.10- manufacture of refined petroleum products, see 19.20- manufacture of industrial gases, see 20.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398927
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.22 
			</td>
		

			<td valign='top'>
				35.2 
			</td>
		

			<td valign='top'>
				Distribution of gaseous fuels through mains 
			</td>
		

			<td valign='top'>
				3520 
			</td>
		

			<td valign='top'>
				This class includes:- distribution and supply of gaseous fuels of all kinds through a system of mains 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- (long-distance) transportation of gases by pipelines, see 49.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398928
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.23 
			</td>
		

			<td valign='top'>
				35.2 
			</td>
		

			<td valign='top'>
				Trade of gas through mains 
			</td>
		

			<td valign='top'>
				3520 
			</td>
		

			<td valign='top'>
				This class includes:- sale of gas to the user through mains- activities of gas brokers or agents that arrange the sale of gas over gas distribution systems operated by others- commodity and transport capacity exchanges for gaseous fuels 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of gaseous fuels, see 46.71- retail sale of bottled gas, see 47.78- direct selling of fuel, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398929
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				35.3 
			</td>
		

			<td valign='top'>
				35 
			</td>
		

			<td valign='top'>
				Steam and air conditioning supply 
			</td>
		

			<td valign='top'>
				353 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398930
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				35.30 
			</td>
		

			<td valign='top'>
				35.3 
			</td>
		

			<td valign='top'>
				Steam and air conditioning supply 
			</td>
		

			<td valign='top'>
				3530 
			</td>
		

			<td valign='top'>
				This class includes:- production, collection and distribution of steam and hot water for heating, power and other purposes- production and distribution of cooled air- production and distribution of chilled water for cooling purposes- production of ice, for food and non-food (e.g. cooling) purposes 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398931
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				E 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				WATER SUPPLY; SEWERAGE, WASTE MANAGEMENT AND REMEDIATION ACTIVITIES 
			</td>
		

			<td valign='top'>
				E 
			</td>
		

			<td valign='top'>
				This section includes activities related to the management (including collection, treatment and disposal) of various forms of waste, such as solid or non-solid industrial or household waste, as well as contaminated sites. The output of the waste or sewage treatment process can either be disposed of or become an input into other production processes. 
			</td>
		

			<td valign='top'>
				Activities of water supply are also grouped in this section, since they are often carried out in connection with, or by units also engaged in, the treatment of sewage. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398932
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				36 
			</td>
		

			<td valign='top'>
				E 
			</td>
		

			<td valign='top'>
				Water collection, treatment and supply 
			</td>
		

			<td valign='top'>
				36 
			</td>
		

			<td valign='top'>
				This division includes the collection, treatment and distribution of water for domestic and industrial needs. Collection of water from various sources, as well as distribution by various means is included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398933
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				36.0 
			</td>
		

			<td valign='top'>
				36 
			</td>
		

			<td valign='top'>
				Water collection, treatment and supply 
			</td>
		

			<td valign='top'>
				360 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398934
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				36.00 
			</td>
		

			<td valign='top'>
				36.0 
			</td>
		

			<td valign='top'>
				Water collection, treatment and supply 
			</td>
		

			<td valign='top'>
				3600 
			</td>
		

			<td valign='top'>
				This class includes water collection, treatment and distribution activities for domestic and industrial needs. Collection of water from various sources, as well as distribution by various means is included.This class includes:- collection of water from rivers, lakes, wells etc.- collection of rain water- purification of water for water supply purposes- treatment of water for industrial and other purposes- desalting of sea or ground water to produce water as the principal product of interest- distribution of water through mains, by trucks or other means- operation of irrigation canals 
			</td>
		

			<td valign='top'>
				The operation of irrigation canals is also included; however the provision of irrigation services through sprinklers, and similar agricultural support services, is not included. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operation of irrigation equipment for agricultural purposes, see 01.61- treatment of wastewater in order to prevent pollution, see 37.00- (long-distance) transport of water via pipelines, see 49.50 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398935
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				37 
			</td>
		

			<td valign='top'>
				E 
			</td>
		

			<td valign='top'>
				Sewerage 
			</td>
		

			<td valign='top'>
				37 
			</td>
		

			<td valign='top'>
				This division includes the operation of sewer systems or sewage treatment facilities that collect, treat, and dispose of sewage. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398936
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				37.0 
			</td>
		

			<td valign='top'>
				37 
			</td>
		

			<td valign='top'>
				Sewerage 
			</td>
		

			<td valign='top'>
				370 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398937
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				37.00 
			</td>
		

			<td valign='top'>
				37.0 
			</td>
		

			<td valign='top'>
				Sewerage 
			</td>
		

			<td valign='top'>
				3700 
			</td>
		

			<td valign='top'>
				This class includes:- operation of sewer systems or sewer treatment facilities- collecting and transporting of human or industrial wastewater from one or several users, as well as rain water by means of sewerage networks, collectors, tanks and other means of transport (sewage vehicles etc.) - emptying and cleaning of cesspools and septic tanks, sinks and pits from sewage; servicing of chemical toilets- treatment of wastewater (including human and industrial wastewater, water from swimming pools etc.) by means of physical, chemical and biological processes like dilution, screening, filtering, sedimentation etc.- maintenance and cleaning of sewers and drains, including sewer rodding 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- decontamination of surface water and groundwater at the place of pollution, see 39.00- cleaning and deblocking of drainpipes in buildings, see 43.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398938
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				38 
			</td>
		

			<td valign='top'>
				E 
			</td>
		

			<td valign='top'>
				Waste collection, treatment and disposal activities; materials recovery 
			</td>
		

			<td valign='top'>
				38 
			</td>
		

			<td valign='top'>
				This division includes the collection, treatment, and disposal of waste materials. 
			</td>
		

			<td valign='top'>
				This also includes local hauling of waste materials and the operation of materials recovery facilities (i.e. those that sort recoverable materials from a waste stream). 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398939
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				38.1 
			</td>
		

			<td valign='top'>
				38 
			</td>
		

			<td valign='top'>
				Waste collection 
			</td>
		

			<td valign='top'>
				381 
			</td>
		

			<td valign='top'>
				This group includes the collection of waste from households and businesses by means of refuse bins, wheeled bins, containers, etc. It includes collection of non-hazardous and hazardous waste e.g. waste from households, used batteries, used cooking oils and fats, waste oil from ships and used oil from garages, as well as construction and demolition waste. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398940
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				38.11 
			</td>
		

			<td valign='top'>
				38.1 
			</td>
		

			<td valign='top'>
				Collection of non-hazardous waste 
			</td>
		

			<td valign='top'>
				3811 
			</td>
		

			<td valign='top'>
				This class includes:- collection of non-hazardous solid waste (i.e. garbage) within a local area, such as collection of waste from households and businesses by means of refuse bins, wheeled bins, containers etc may include mixed recoverable materials- collection of recyclable materials- collection of refuse in litter-bins in public places 
			</td>
		

			<td valign='top'>
				This class also includes:- collection of construction and demolition waste- collection and removal of debris such as brush and rubble- collection of waste output of textile mills- operation of waste transfer facilities for non-hazardous waste 
			</td>
		

			<td valign='top'>
				- Activities of recycling centres for non-hazardous waste 
			</td>
		

			<td valign='top'>
				This class excludes:- collection of hazardous waste, see 38.12- operation of landfills for the disposal of non-hazardous waste, see 38.21- operation of facilities where commingled recoverable materials such as paper, plastics, etc. are sorted into distinct categories, see 38.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398941
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				38.12 
			</td>
		

			<td valign='top'>
				38.1 
			</td>
		

			<td valign='top'>
				Collection of hazardous waste 
			</td>
		

			<td valign='top'>
				3812 
			</td>
		

			<td valign='top'>
				This class includes the collection of solid and non-solid hazardous waste, i.e. explosive, oxidizing, flammable, toxic, irritant, carcinogenic, corrosive, infectious and other substances and preparations harmful for human health and environment. It may also entail identification, treatment, packaging and labeling of waste for the purposes of transport.This class includes:- collection of hazardous waste, such as:  • used oil from shipment or garages  • bio-hazardous waste  • nuclear waste  • used batteries etc.- operation of waste transfer stations for hazardous waste 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- remediation and clean up of contaminated buildings, mine sites, soil, ground water, e.g. asbestos removal, see 39.00 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398942
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				38.2 
			</td>
		

			<td valign='top'>
				38 
			</td>
		

			<td valign='top'>
				Waste treatment and disposal 
			</td>
		

			<td valign='top'>
				382 
			</td>
		

			<td valign='top'>
				This group includes the disposal and treatment prior to disposal of various forms of waste by different means, such as treatment of organic waste with the aim of disposal; treatment and disposal of toxic live or dead animals and other contaminated waste; treatment and disposal of transition radioactive waste from hospitals, etc.; dumping of refuse on land or in water; burial or ploughing-under of refuse; disposal of used goods such as refrigerators to eliminate harmful waste; disposal of waste by incineration or combustion. Included is also energy recovery resulting from waste incineration process. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- treatment and disposal of wastewater, see 37.00- materials recovery, see 38.3 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398943
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				38.21 
			</td>
		

			<td valign='top'>
				38.2 
			</td>
		

			<td valign='top'>
				Treatment and disposal of non-hazardous waste 
			</td>
		

			<td valign='top'>
				3821 
			</td>
		

			<td valign='top'>
				This class includes the disposal and treatment prior to disposal of solid or non-solid non-hazardous waste:- operation of landfills for the disposal of non-hazardous waste- disposal of non-hazardous waste by combustion or incineration or other methods, with or without the resulting production of electricity or steam, compost, substitute fuels, biogas, ashes or other by-products for further use etc.- treatment of organic waste for disposal 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- incineration and combustion of hazardous waste, see 38.22- operation of facilities where commingled recoverable materials such as paper, plastics, used beverage cans and metals, are sorted into distinct categories, see 38.32- decontamination, clean up of land, water; toxic material abatement, see 39.00 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398944
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				38.22 
			</td>
		

			<td valign='top'>
				38.2 
			</td>
		

			<td valign='top'>
				Treatment and disposal of hazardous waste 
			</td>
		

			<td valign='top'>
				3822 
			</td>
		

			<td valign='top'>
				This class includes the disposal and treatment prior to disposal of solid or non-solid hazardous waste, including waste that if explosive, oxidising, flammable, toxic, irritant, carcinogenic, corrosive, infectious and other substances and preparations harmful for human health and environment.This class includes:- operation of facilities for treatment of hazardous waste- treatment and disposal of toxic live or dead animals and other contaminated waste- incineration of hazardous waste- disposal of used goods such as refrigerators to eliminate harmful waste- treatment, disposal and storage of radioactive nuclear waste including:   • treatment and disposal of transition radioactive waste, i.e. decaying within the period of transport, from hospitals  • encapsulation, preparation and other treatment of nuclear waste for storage 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- reprocessing of nuclear fuels, see 20.13- incineration of non-hazardous waste, see 38.21- decontamination, clean up of land, water; toxic material abatement, see 39.00 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398945
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				38.3 
			</td>
		

			<td valign='top'>
				38 
			</td>
		

			<td valign='top'>
				Materials recovery 
			</td>
		

			<td valign='top'>
				383 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398946
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				38.31 
			</td>
		

			<td valign='top'>
				38.3 
			</td>
		

			<td valign='top'>
				Dismantling of wrecks 
			</td>
		

			<td valign='top'>
				3830 
			</td>
		

			<td valign='top'>
				This class includes dismantling of wrecks of any type (automobiles, ships, computers, televisions and other equipment) for materials recovery. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- disposal of used goods such as refrigerators to eliminate harmful waste, see 38.22- dismantling of automobiles, ships, computers, televisions and other equipment to obtain re-sell usable parts, see section G 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398947
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				38.32 
			</td>
		

			<td valign='top'>
				38.3 
			</td>
		

			<td valign='top'>
				Recovery of sorted materials 
			</td>
		

			<td valign='top'>
				3830 
			</td>
		

			<td valign='top'>
				This class includes the processing of metal and non-metal waste and scrap and other articles into secondary raw materials, usually involving a mechanical or chemical transformation process.Examples of the mechanical or chemical transformation processes that are undertaken are:- mechanical crushing of metal waste from used cars, washing machines, bikes etc.- mechanical reduction of large iron pieces such as railway wagons- shredding of metal waste, end-of-life vehicles etc.- other methods of mechanical treatment as cutting, pressing to reduce the volume- reclaiming metals out of photographic waste, e.g. fixer solution or photographic films and paper - reclaiming of rubber such as used tyres to produce secondary raw material- sorting and pelleting of plastics to produce secondary raw material for tubes, flower pots, pallets and the like- processing (cleaning, melting, grinding) of plastic or rubber waste to granulates- crushing, cleaning and sorting of glass- crushing, cleaning and sorting of other waste such as demolition waste to obtain secondary raw material- processing of used cooking oils and fats into secondary raw materials- processing of other food, beverage and tobacco waste and residual substances into secondary raw materials 
			</td>
		

			<td valign='top'>
				Also included is the recovery of materials from waste streams in the form of (1) separating and sorting recoverable materials from non-hazardous waste streams (i.e. garbage) or (2) the separating and sorting of commingled recoverable materials, such as paper, plastics, used beverage cans and metals, into distinct categories. 
			</td>
		

			<td valign='top'>
				- Rubber powder from rubber waste and scrap, including decontamination, division (elimination) of fractions and devulcanising- Extraction of silver from waste chemicals, other than by electrolytic refining- Treatment and disposal, as well as recovery, of waste oil- Production of iron from clinker 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of new final products from (whether or not self-manufactured) secondary raw materials, such as spinning yarn from garnetted stock, making pulp from paper waste, retreading tyres or production of metal from metal scrap, see corresponding classes in section C (Manufacturing)- reprocessing of nuclear fuels, see 20.13- remelting ferrous waste and scrap, see 24.10- materials recovery during waste combustion or incineration process, see 38.2- treatment and disposal of non-hazardous waste, see 38.21- treatment of organic waste for disposal, including production of compost, see 38.21- energy recovery during non-hazardous waste incineration process, see 38.21- treatment and disposal of transition radioactive waste from hospitals etc., see 38.22- treatment and disposal of toxic, contaminated waste, see 38.22- wholesale of recoverable materials, see 46.77 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398948
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				39 
			</td>
		

			<td valign='top'>
				E 
			</td>
		

			<td valign='top'>
				Remediation activities and other waste management services 
			</td>
		

			<td valign='top'>
				39 
			</td>
		

			<td valign='top'>
				This division includes the provision of remediation services, i.e. the cleanup of contaminated buildings and sites, soil, surface or ground water. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398949
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				39.0 
			</td>
		

			<td valign='top'>
				39 
			</td>
		

			<td valign='top'>
				Remediation activities and other waste management services 
			</td>
		

			<td valign='top'>
				390 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398950
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				39.00 
			</td>
		

			<td valign='top'>
				39.0 
			</td>
		

			<td valign='top'>
				Remediation activities and other waste management services 
			</td>
		

			<td valign='top'>
				3900 
			</td>
		

			<td valign='top'>
				This class includes:- decontamination of soils and groundwater at the place of pollution, either in situ or ex situ, using e.g. mechanical, chemical or biological methods- decontamination of industrial plants or sites, including nuclear plants and sites- decontamination and cleaning up of surface water following accidental pollution, e.g. through collection of pollutants or through application of chemicals- cleaning up oil spills and other pollutions on land, in surface water, in ocean and seas, including coastal areas- asbestos, lead paint, and other toxic material abatement- clearing of landmines and the like (including detonation)- other specialised pollution-control activities 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- pest control in agriculture, see 01.61- purification of water for water supply purposes, see 36.00- treatment and disposal of non-hazardous waste, see 38.21- treatment and disposal of hazardous waste, see 38.22- outdoor sweeping and watering of streets etc., see 81.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398951
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				F 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				CONSTRUCTION 
			</td>
		

			<td valign='top'>
				F 
			</td>
		

			<td valign='top'>
				This section includes general construction and specialised construction activities for buildings and civil engineering works. It includes new work, repair, additions and alterations, the erection of prefabricated buildings or structures on the site and also construction of a temporary nature. General construction is the construction of entire dwellings, office buildings, stores and other public and utility buildings, farm buildings etc., or the construction of civil engineering works such as motorways, streets, bridges, tunnels, railways, airfields, harbours and other water projects, irrigation systems, sewerage systems, industrial facilities, pipelines and electric lines, sports facilities etc. This work can be carried out on own account or on a fee or contract basis. Portions of the work and sometimes even the whole practical work can be subcontracted out. A unit that carries the overall responsibility for a construction project is classified here.Also included is the repair of buildings and engineering works.This section includes the complete construction of buildings (division 41), the complete construction of civil engineering works (division 42), as well as specialised construction activities, if carried out only as a part of the construction process (division 43).The rental of construction equipment with operator is classified with the specific construction activity carried out with this equipment. 
			</td>
		

			<td valign='top'>
				This section also includes the development of building projects for buildings or civil engineering works by bringing together financial, technical and physical means to realise the construction projects for later sale. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				If these activities are carried out not for later sale of the construction projects, but for their operation (e.g. rental of space in these buildings, manufacturing activities in these plants), the unit would not be classified here, but according to its operational activity, i.e. real estate, manufacturing etc. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398952
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				41 
			</td>
		

			<td valign='top'>
				F 
			</td>
		

			<td valign='top'>
				Construction of buildings 
			</td>
		

			<td valign='top'>
				41 
			</td>
		

			<td valign='top'>
				This division includes general construction of buildings of all kinds. It includes new work, repair, additions and alterations, the erection of pre-fabricated buildings or structures on the site and also construction of temporary nature. Included is the construction of entire dwellings, office buildings, stores and other public and utility buildings, farm buildings, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398953
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				41.1 
			</td>
		

			<td valign='top'>
				41 
			</td>
		

			<td valign='top'>
				Development of building projects 
			</td>
		

			<td valign='top'>
				410 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398954
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				41.10 
			</td>
		

			<td valign='top'>
				41.1 
			</td>
		

			<td valign='top'>
				Development of building projects 
			</td>
		

			<td valign='top'>
				4100 
			</td>
		

			<td valign='top'>
				This class includes:- development of building projects for residential and non-residential buildings by bringing together financial, technical and physical means to realise the building projects for later sale 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Building co-operatives, terminating their activity when the construction of the buildings is finalised 
			</td>
		

			<td valign='top'>
				This class excludes:- construction of buildings, see 41.20- architectural and engineering activities, see 71.1- project management services related to building projects, see 71.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398955
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				41.2 
			</td>
		

			<td valign='top'>
				41 
			</td>
		

			<td valign='top'>
				Construction of residential and non-residential buildings 
			</td>
		

			<td valign='top'>
				410 
			</td>
		

			<td valign='top'>
				This group includes the construction of complete residential or non-residential buildings, on own account for sale or on a fee or contract basis. Outsourcing parts or even the whole construction process is possible. If only specialised parts of the construction process are carried out, the activity is classified in division 43. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398956
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				41.20 
			</td>
		

			<td valign='top'>
				41.2 
			</td>
		

			<td valign='top'>
				Construction of residential and non-residential buildings 
			</td>
		

			<td valign='top'>
				4100 
			</td>
		

			<td valign='top'>
				This class includes:- construction of all types of residential buildings:  • single-family houses  • multi-family buildings, including high-rise buildings- construction of all types of non-residential buildings:  • buildings for industrial production, e.g. factories, workshops, assembly plants etc.  • hospitals, schools, office buildings  • hotels, stores, shopping malls, restaurants  • airport buildings  • indoor sports facilities  • parking garages, including underground parking garages  • warehouses  • religious buildings- assembly and erection of prefabricated constructions on the site 
			</td>
		

			<td valign='top'>
				This class also includes:- remodelling or renovating existing residential structures 
			</td>
		

			<td valign='top'>
				- Assembly of silos- Building and installation of grain storage systems 
			</td>
		

			<td valign='top'>
				This class excludes:- construction of industrial facilities, except buildings, see 42.99- architectural and engineering activities, see 71.1- project management for construction, see 71.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398957
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				42 
			</td>
		

			<td valign='top'>
				F 
			</td>
		

			<td valign='top'>
				Civil engineering 
			</td>
		

			<td valign='top'>
				42 
			</td>
		

			<td valign='top'>
				This division includes general construction for civil engineering objects. It includes new work, repair, additions and alterations, the erection of pre-fabricated structures on the site and also construction of temporary nature. Included is the construction of heavy constructions such as motorways, streets, bridges, tunnels, railways, airfields, harbours and other water projects, irrigation systems, sewerage systems, industrial facilities, pipelines and electric lines, outdoor sports facilities, etc. This work can be carried out on own account or on a fee or contract basis. Portions of the work and sometimes even the whole practical work can be subcontracted out. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398958
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				42.1 
			</td>
		

			<td valign='top'>
				42 
			</td>
		

			<td valign='top'>
				Construction of roads and railways 
			</td>
		

			<td valign='top'>
				421 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398959
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.11 
			</td>
		

			<td valign='top'>
				42.1 
			</td>
		

			<td valign='top'>
				Construction of roads and motorways 
			</td>
		

			<td valign='top'>
				4210 
			</td>
		

			<td valign='top'>
				This class includes:- construction of motorways, streets, roads, other vehicular and pedestrian ways- surface work on streets, roads, highways, bridges or tunnels:  • asphalt paving of roads  • road painting and other marking  • installation of crash barriers, traffic signs and the like- construction of airfield runways 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of street lighting and electrical signals, see 43.21- architectural and engineering activities, see 71.1- project management for construction, see 71.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398960
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.12 
			</td>
		

			<td valign='top'>
				42.1 
			</td>
		

			<td valign='top'>
				Construction of railways and underground railways 
			</td>
		

			<td valign='top'>
				4210 
			</td>
		

			<td valign='top'>
				This class includes:- construction of railways and subways 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of lighting and electrical signals, see 43.21- architectural and engineering activities, see 71.1- project management for construction, see 71.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398961
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.13 
			</td>
		

			<td valign='top'>
				42.1 
			</td>
		

			<td valign='top'>
				Construction of bridges and tunnels 
			</td>
		

			<td valign='top'>
				4210 
			</td>
		

			<td valign='top'>
				This class includes:- construction of bridges, including those for elevated highways - construction of tunnels 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of lighting and electrical signals, see 43.21- architectural and engineering activities, see 71.1- project management for construction, see 71.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398962
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				42.2 
			</td>
		

			<td valign='top'>
				42 
			</td>
		

			<td valign='top'>
				Construction of utility projects 
			</td>
		

			<td valign='top'>
				422 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398963
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.21 
			</td>
		

			<td valign='top'>
				42.2 
			</td>
		

			<td valign='top'>
				Construction of utility projects for fluids 
			</td>
		

			<td valign='top'>
				4220 
			</td>
		

			<td valign='top'>
				This class includes the construction of distribution lines for transportation of fluids and related buildings and structures that are integral part of these systems.This class includes:- construction of civil engineering constructions for:  • long-distance and urban pipelines  • water main and line construction  • irrigation systems (canals)  • reservoirs- construction of:  • sewer systems, including repair  • sewage disposal plants  • pumping stations 
			</td>
		

			<td valign='top'>
				This class also includes:- water well drilling 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- project management activities related to civil engineering works, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398964
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.22 
			</td>
		

			<td valign='top'>
				42.2 
			</td>
		

			<td valign='top'>
				Construction of utility projects for electricity and telecommunications 
			</td>
		

			<td valign='top'>
				4220 
			</td>
		

			<td valign='top'>
				This class includes the construction of distribution lines for electricity and telecommunications and related buildings and structures that are integral part of these systems.This class includes:- construction of civil engineering constructions for:  • long-distance and urban communication and power lines  • power plants 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Installation of solar plants- Installation of wind energy plants 
			</td>
		

			<td valign='top'>
				This class excludes:- project management activities related to civil engineering works, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398965
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				42.9 
			</td>
		

			<td valign='top'>
				42 
			</td>
		

			<td valign='top'>
				Construction of other civil engineering projects 
			</td>
		

			<td valign='top'>
				429 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398966
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.91 
			</td>
		

			<td valign='top'>
				42.9 
			</td>
		

			<td valign='top'>
				Construction of water projects 
			</td>
		

			<td valign='top'>
				4290 
			</td>
		

			<td valign='top'>
				This class includes:- construction of:  • waterways, harbour and river works, pleasure ports (marinas), locks, etc.  • dams and dykes- dredging of waterways 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- project management activities related to civil engineering works, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398967
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				42.99 
			</td>
		

			<td valign='top'>
				42.9 
			</td>
		

			<td valign='top'>
				Construction of other civil engineering projects n.e.c. 
			</td>
		

			<td valign='top'>
				4290 
			</td>
		

			<td valign='top'>
				This class includes:- construction of industrial facilities, except buildings, such as:  • refineries  • chemical plants- construction work, other than buildings, such as:  • outdoor sports facilities 
			</td>
		

			<td valign='top'>
				This class also includes:- land subdivision with land improvement (e.g. adding of roads, utility infrastructure etc.) 
			</td>
		

			<td valign='top'>
				- Go-kart track installation activities 
			</td>
		

			<td valign='top'>
				This class excludes:- installation of industrial machinery and equipment, see 33.20- land subdivision without land improvement, see 68.10- project management activities related to civil engineering works, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398968
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				43 
			</td>
		

			<td valign='top'>
				F 
			</td>
		

			<td valign='top'>
				Specialised construction activities 
			</td>
		

			<td valign='top'>
				43 
			</td>
		

			<td valign='top'>
				This division includes specialised construction activities (special trades), i.e. the construction of parts of buildings and civil engineering works or preparation therefore. These activities are usually specialised in one aspect common to different structures, requiring specialised skills or equipment, such as pile-driving, foundation work, carcass work, concrete work, brick laying, stone setting, scaffolding, roof covering, etc. The erection of steel structures is included, provided that the parts are not produced by the same unit. Specialised construction activities are mostly carried out under subcontract, but especially in repair construction it is done directly for the owner of the property.Included is the installation of all kind of utilities that make the construction function as such. These activities are usually performed at the site of the construction, although parts of the job may be carried out in a special shop. Included are activities such as plumbing, installation of heating and air-conditioning systems, antennas, alarm systems and other electrical work, sprinkler systems, elevators and escalators, etc. Also included are insulation work (water, heat, sound), sheet metal work, commercial refrigerating work, the installation of illumination and signalling systems for roads, railways, airports, harbours, etc. Also repair of the same type as the above mentioned activities is included. Building completion activities encompass activities that contribute to the completion or finishing of a construction such as glazing, plastering, painting, floor and wall tiling or covering with other materials like parquet, carpets, wallpaper, etc., floor sanding, finish carpentry, acoustical work, cleaning of the exterior, etc. Also repair of the same type as the above mentioned activities is included.The rental of equipment with operator is classified with the associated construction activity. 
			</td>
		

			<td valign='top'>
				Also included are building finishing and building completion activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398969
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				43.1 
			</td>
		

			<td valign='top'>
				43 
			</td>
		

			<td valign='top'>
				Demolition and site preparation 
			</td>
		

			<td valign='top'>
				431 
			</td>
		

			<td valign='top'>
				This group includes activities of preparing a site for subsequent construction activities, including the removal of previously existing structures. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398970
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.11 
			</td>
		

			<td valign='top'>
				43.1 
			</td>
		

			<td valign='top'>
				Demolition 
			</td>
		

			<td valign='top'>
				4311 
			</td>
		

			<td valign='top'>
				This class includes:- demolition or wrecking of buildings and other structures 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398971
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.12 
			</td>
		

			<td valign='top'>
				43.1 
			</td>
		

			<td valign='top'>
				Site preparation 
			</td>
		

			<td valign='top'>
				4312 
			</td>
		

			<td valign='top'>
				This class includes:- clearing of building sites- earth moving: excavation, landfill, levelling and grading of construction sites, trench digging, rock removal, blasting, etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- site preparation for mining:  • overburden removal and other development and preparation of mineral properties and sites, except oil and gas sites- building site drainage- drainage of agricultural or forestry land 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- drilling of production oil or gas wells, see 06.10, 06.20- decontamination of soil, see 39.00- water well drilling, see 42.21- shaft sinking, see 43.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398972
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.13 
			</td>
		

			<td valign='top'>
				43.1 
			</td>
		

			<td valign='top'>
				Test drilling and boring 
			</td>
		

			<td valign='top'>
				4312 
			</td>
		

			<td valign='top'>
				This class includes:- test drilling, test boring and core sampling for construction, geophysical, geological or similar purposes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- drilling of production oil or gas wells, see 06.10, 06.20- test drilling and boring support services during mining activities, see 09.90- water well drilling, see 42.21- shaft sinking, see 43.99- oil and gas field exploration, geophysical, geological and seismic surveying, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398973
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				43.2 
			</td>
		

			<td valign='top'>
				43 
			</td>
		

			<td valign='top'>
				Electrical, plumbing and other construction installation activities 
			</td>
		

			<td valign='top'>
				432 
			</td>
		

			<td valign='top'>
				This group includes installation activities that support the functioning of a building as such, including installation of electrical systems, plumbing (water, gas and sewage systems), heat and air-conditioning systems, elevators etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398974
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.21 
			</td>
		

			<td valign='top'>
				43.2 
			</td>
		

			<td valign='top'>
				Electrical installation 
			</td>
		

			<td valign='top'>
				4321 
			</td>
		

			<td valign='top'>
				This class includes the installation of electrical systems in all kinds of buildings and civil engineering structures of electrical systems.This class includes:- installation of:  • electrical wiring and fittings  • telecommunications wiring  • computer network and cable television wiring, including fibre optic  • satellite dishes  • lighting systems  • fire alarms  • burglar alarm systems  • street lighting and electrical signals  • airport runway lighting  • electric solar energy collectors 
			</td>
		

			<td valign='top'>
				This class also includes:- connecting of electric appliances and household equipment, including baseboard heating 
			</td>
		

			<td valign='top'>
				- Specialised electrical installation for exhibition stands 
			</td>
		

			<td valign='top'>
				This class excludes:- construction of communications and power transmission lines, see 42.22- monitoring and remote monitoring of electronic security systems, such as burglar alarms and fire alarms, including their installation and maintenance, see 80.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398975
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.22 
			</td>
		

			<td valign='top'>
				43.2 
			</td>
		

			<td valign='top'>
				Plumbing, heat and air-conditioning installation 
			</td>
		

			<td valign='top'>
				4322 
			</td>
		

			<td valign='top'>
				This class includes the installation of plumbing, heating and air-conditioning systems, including additions, alterations, maintenance and repair.This class includes:- installation in buildings or other construction projects of:  • heating systems (electric, gas and oil)  • furnaces, cooling towers  • non-electric solar energy collectors  • plumbing and sanitary equipment  • ventilation and air-conditioning equipment and ducts  • gas fittings  • steam piping  • fire sprinkler systems  • lawn sprinkler systems- duct work installation 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Installation of heat exchanger systems 
			</td>
		

			<td valign='top'>
				This class excludes:- installation of electric baseboard heating, see 43.21 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398976
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.29 
			</td>
		

			<td valign='top'>
				43.2 
			</td>
		

			<td valign='top'>
				Other construction installation 
			</td>
		

			<td valign='top'>
				4329 
			</td>
		

			<td valign='top'>
				This class includes the installation of equipment other than electrical, plumbing, heating and air-conditioning systems or industrial machinery in buildings and civil engineering structures.This class includes:- installation in buildings or other construction projects of:  • elevators, escalators, including repair and maintenance  • automated and revolving doors  • lightning conductors  • vacuum cleaning systems  • thermal, sound or vibration insulation 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of industrial machinery, see 33.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398977
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				43.3 
			</td>
		

			<td valign='top'>
				43 
			</td>
		

			<td valign='top'>
				Building completion and finishing 
			</td>
		

			<td valign='top'>
				433 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398978
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.31 
			</td>
		

			<td valign='top'>
				43.3 
			</td>
		

			<td valign='top'>
				Plastering 
			</td>
		

			<td valign='top'>
				4330 
			</td>
		

			<td valign='top'>
				This class includes:- application in buildings or other construction projects of interior and exterior plaster or stucco, including related lathing materials 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398979
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.32 
			</td>
		

			<td valign='top'>
				43.3 
			</td>
		

			<td valign='top'>
				Joinery installation 
			</td>
		

			<td valign='top'>
				4330 
			</td>
		

			<td valign='top'>
				This class includes:- installation of doors (except automated and revolving), windows, door and window frames, of wood or other materials- installation of fitted kitchens, built-in cupboards, staircases, shop fittings and the like- interior completion such as ceilings, movable partitions, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of automated and revolving doors, see 43.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398980
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.33 
			</td>
		

			<td valign='top'>
				43.3 
			</td>
		

			<td valign='top'>
				Floor and wall covering 
			</td>
		

			<td valign='top'>
				4330 
			</td>
		

			<td valign='top'>
				This class includes:- laying, tiling, hanging or fitting in buildings or other construction projects of:  • ceramic, concrete or cut stone wall or floor tiles, ceramic stove fitting  • parquet and other wooden floor coverings, wooden wall coverings  • carpets and linoleum floor coverings, including of rubber or plastic  • terrazzo, marble, granite or slate floor or wall coverings  • wallpaper 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398981
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.34 
			</td>
		

			<td valign='top'>
				43.3 
			</td>
		

			<td valign='top'>
				Painting and glazing 
			</td>
		

			<td valign='top'>
				4330 
			</td>
		

			<td valign='top'>
				This class includes:- interior and exterior painting of buildings- painting of civil engineering structures- installation of glass, mirrors, etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Covering windows and glazings with cling film 
			</td>
		

			<td valign='top'>
				This class excludes:- installation of windows, see 43.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398982
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.39 
			</td>
		

			<td valign='top'>
				43.3 
			</td>
		

			<td valign='top'>
				Other building completion and finishing 
			</td>
		

			<td valign='top'>
				4330 
			</td>
		

			<td valign='top'>
				This class includes:- cleaning of new buildings after construction- other building completion and finishing work n.e.c. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Installation and mounting in buildings of window sun filter 
			</td>
		

			<td valign='top'>
				This class excludes:- activities of interior decoration designers, see 74.10- general interior cleaning of buildings and other structures, see 81.21- specialised interior and exterior cleaning of buildings, see 81.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398983
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				43.9 
			</td>
		

			<td valign='top'>
				43 
			</td>
		

			<td valign='top'>
				Other specialised construction activities 
			</td>
		

			<td valign='top'>
				439 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398984
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.91 
			</td>
		

			<td valign='top'>
				43.9 
			</td>
		

			<td valign='top'>
				Roofing activities 
			</td>
		

			<td valign='top'>
				4390 
			</td>
		

			<td valign='top'>
				This class includes:- erection of roofs- roof covering 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of construction machinery and equipment without operator, see 77.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398985
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				43.99 
			</td>
		

			<td valign='top'>
				43.9 
			</td>
		

			<td valign='top'>
				Other specialised construction activities n.e.c. 
			</td>
		

			<td valign='top'>
				4390 
			</td>
		

			<td valign='top'>
				This class includes:- construction activities specialising in one aspect common to different kind of structures, requiring specialised skill or equipment:  • construction of foundations, including pile driving  • damp proofing and water proofing works  • de-humidification of buildings  • shaft sinking  • erection of steel elements  • steel bending  • bricklaying and stone setting  • scaffolds and work platform erecting and dismantling, excluding rental of scaffolds and work platforms  • erection of chimneys and industrial ovens  • work with specialist access requirements necessitating climbing skills and the use of related equipment, e.g. working at height on tall structures- subsurface work- construction of outdoor swimming pools- steam cleaning, sand blasting and similar activities for building exteriors- rental of cranes and other building equipment, which cannot be allocated to a specific construction type, with operator 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of construction machinery and equipment without operator, see 77.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398986
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				G 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				WHOLESALE AND RETAIL TRADE; REPAIR OF MOTOR VEHICLES AND MOTORCYCLES 
			</td>
		

			<td valign='top'>
				G 
			</td>
		

			<td valign='top'>
				This section includes wholesale and retail sale (i.e. sale without transformation) of any type of goods, and rendering services incidental to the sale of merchandise. Wholesaling and retailing are the final steps in the distribution of merchandise. Also included in this section are the repair of motor vehicles and motorcycles.Sale without transformation is considered to include the usual operations (or manipulations) associated with trade, for example sorting, grading and assembling of goods, mixing (blending) of goods (for example sand), bottling (with or without preceding bottle cleaning), packing, breaking bulk and repacking for distribution in smaller lots, storage (whether or not frozen or chilled).Division 45 includes all activities related to the sale and repair of motor vehicles and motorcycles, while divisions 46 and 47 include all other sale activities. The distinction between division 46 (wholesale) and division 47 (retail sale) is based on the predominant type of customer.Wholesale is the resale (sale without transformation) of new and used goods to retailers, business-to-business trade, such as to industrial, commercial, institutional or professional users, or resale to other wholesalers, or involves acting as an agent or broker in buying merchandise for, or selling merchandise to, such persons or companies. The principal types of businesses included are merchant wholesalers, i.e. wholesalers who take title to the goods they sell, such as wholesale merchants or jobbers, industrial distributors, exporters, importers, and cooperative buying associations, sales branches and sales offices (but not retail stores) that are maintained by manufacturing or mining units apart from their plants or mines for the purpose of marketing their products and that do not merely take orders to be filled by direct shipments from the plants or mines. Also included are merchandise and commodity brokers, commission merchants and agents and assemblers, buyers and cooperative associations engaged in the marketing of farm products. Wholesalers frequently physically assemble, sort and grade goods in large lots, break bulk, repack and redistribute in smaller lots, for example pharmaceuticals; store, refrigerate, deliver and install goods, engage in sales promotion for their customers and label design.Retailing is the resale (sale without transformation) of new and used goods mainly to the general public for personal or household consumption or utilisation, in shops, department stores, stalls, mail-order houses, door-to-door sales persons, hawkers, consumer cooperatives, auction houses etc. Most retailers take title to the goods they sell, but some act as agents for a principal and sell either on consignment or on a commission basis. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398987
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				45 
			</td>
		

			<td valign='top'>
				G 
			</td>
		

			<td valign='top'>
				Wholesale and retail trade and repair of motor vehicles and motorcycles 
			</td>
		

			<td valign='top'>
				45 
			</td>
		

			<td valign='top'>
				This division includes all activities (except manufacture and rental) related to motor vehicles and motorcycles, including lorries and trucks, such as the wholesale and retail sale of new and second-hand vehicles, the repair and maintenance of vehicles and the wholesale and retail sale of parts and accessories for motor vehicles and motorcycles. Also included are activities of commission agents involved in wholesale or retail sale of vehicles. 
			</td>
		

			<td valign='top'>
				This division also includes activities such as washing, polishing of vehicles etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division does not include the retail sale of automotive fuel and lubricating or cooling products or the rental of motor vehicles or motorcycles. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398988
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				45.1 
			</td>
		

			<td valign='top'>
				45 
			</td>
		

			<td valign='top'>
				Sale of motor vehicles 
			</td>
		

			<td valign='top'>
				451 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398989
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				45.11 
			</td>
		

			<td valign='top'>
				45.1 
			</td>
		

			<td valign='top'>
				Sale of cars and light motor vehicles 
			</td>
		

			<td valign='top'>
				4510 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale and retail sale of new and used vehicles:  • passenger motor vehicles, including specialised passenger motor vehicles such as ambulances and minibuses, etc. (with a weight not exceeding 3,5 tons) 
			</td>
		

			<td valign='top'>
				This class also includes:- wholesale and retail sale of off-road motor vehicles (with a weight not exceeding 3,5 tons) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale and retail sale of parts and accessories for motor vehicles, see 45.3- rental of motor vehicles with driver, see 49.3- rental of motor vehicles without driver, see 77.1 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398990
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				45.19 
			</td>
		

			<td valign='top'>
				45.1 
			</td>
		

			<td valign='top'>
				Sale of other motor vehicles 
			</td>
		

			<td valign='top'>
				4510 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale and retail sale of new and used vehicles:  • lorries, trailers and semi-trailers  • camping vehicles such as caravans and motor homes 
			</td>
		

			<td valign='top'>
				This class also includes:- wholesale and retail sale of off-road motor vehicles (with a weight exceeding 3,5 tons) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale and retail sale of parts and accessories for motor vehicles, see 45.3- rental of trucks with driver, see 49.41- rental of trucks without driver, see 77.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398991
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				45.2 
			</td>
		

			<td valign='top'>
				45 
			</td>
		

			<td valign='top'>
				Maintenance and repair of motor vehicles 
			</td>
		

			<td valign='top'>
				452 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398992
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				45.20 
			</td>
		

			<td valign='top'>
				45.2 
			</td>
		

			<td valign='top'>
				Maintenance and repair of motor vehicles 
			</td>
		

			<td valign='top'>
				4520 
			</td>
		

			<td valign='top'>
				This class includes:- maintenance and repair of motor vehicles:  • mechanical repairs  • electrical repairs  • electronic injection systems repair  • ordinary servicing  • bodywork repair  • repair of motor vehicle parts  • washing, polishing, etc.  • spraying and painting  • repair of screens and windows  • repair of motor vehicle seats- tyre and tube repair, fitting or replacement- anti-rust treatment- installation of parts and accessories not as part of the manufacturing process 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Repair and maintenance of trailers and semi-trailers- Tyre service activities 
			</td>
		

			<td valign='top'>
				This class excludes:- retreading and rebuilding of tyres, see 22.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398993
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				45.3 
			</td>
		

			<td valign='top'>
				45 
			</td>
		

			<td valign='top'>
				Sale of motor vehicle parts and accessories 
			</td>
		

			<td valign='top'>
				453 
			</td>
		

			<td valign='top'>
				This group includes wholesale and retail trade of all kinds of parts, components, supplies, tools and accessories for motor vehicles, such as:  • rubber tyres and inner tubes for tyres  • spark plugs, batteries, lighting equipment and electrical parts 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398994
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				45.31 
			</td>
		

			<td valign='top'>
				45.3 
			</td>
		

			<td valign='top'>
				Wholesale trade of motor vehicle parts and accessories 
			</td>
		

			<td valign='top'>
				4530 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398995
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				45.32 
			</td>
		

			<td valign='top'>
				45.3 
			</td>
		

			<td valign='top'>
				Retail trade of motor vehicle parts and accessories 
			</td>
		

			<td valign='top'>
				4530 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of automotive fuel, see 47.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398996
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				45.4 
			</td>
		

			<td valign='top'>
				45 
			</td>
		

			<td valign='top'>
				Sale, maintenance and repair of motorcycles and related parts and accessories 
			</td>
		

			<td valign='top'>
				454 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			398997
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				45.40 
			</td>
		

			<td valign='top'>
				45.4 
			</td>
		

			<td valign='top'>
				Sale, maintenance and repair of motorcycles and related parts and accessories 
			</td>
		

			<td valign='top'>
				4540 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale and retail sale of motorcycles, including mopeds- wholesale and retail sale of parts and accessories for motorcycles (including by commission agents and mail order houses) - maintenance and repair of motorcycles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of bicycles and related parts and accessories, see 46.49- retail sale of bicycles and related parts and accessories, see 47.64- rental of motorcycles, see 77.39- repair and maintenance of bicycles, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398998
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				G 
			</td>
		

			<td valign='top'>
				Wholesale trade, except of motor vehicles and motorcycles 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				This division includes wholesale trade on own account or on a fee or contract basis (commission trade) related to domestic wholesale trade as well as international wholesale trade (import/export). 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- wholesale of motor vehicles, caravans and motorcycles, see 45.1, 45.4- wholesale of motor vehicle accessories, see 45.31, 45.40- rental and leasing of goods, see division 77- packing of solid goods and bottling of liquid or gaseous goods, including blending and filtering for third parties, see 82.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			398999
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Wholesale on a fee or contract basis 
			</td>
		

			<td valign='top'>
				461 
			</td>
		

			<td valign='top'>
				This group includes:- activities of commission agents, commodity brokers and all other wholesalers who trade on behalf and on the account of others- activities of those involved in bringing sellers and buyers together or undertaking commercial transactions on behalf of a principal, including on the Internet. 
			</td>
		

			<td valign='top'>
				This group also includes:- activities of wholesale auctioneering houses, including Internet wholesale auctions 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399000
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.11 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of agricultural raw materials, live animals, textile raw materials and semi-finished goods 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399001
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.12 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of fuels, ores, metals and industrial chemicals 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		

			<td valign='top'>
				This class includes agents involved in the sale of:  • fuels, ores, metals and industrial chemicals, including fertilisers 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399002
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.13 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of timber and building materials 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399003
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.14 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of machinery, industrial equipment, ships and aircraft 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		

			<td valign='top'>
				This class includes agents involved in the sale of:  • machinery, including office machinery and computers, industrial equipment, ships and aircraft 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of commission agents for motor vehicles, see 45.1- auctions of motor vehicles, see 45.1- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399004
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.15 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of furniture, household goods, hardware and ironmongery 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399005
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.16 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of textiles, clothing, fur, footwear and leather goods 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399006
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.17 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of food, beverages and tobacco 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399007
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.18 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents specialised in the sale of other particular products 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99- activities of insurance agents, see 66.22- activities of real estate agents, see 68.31 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399008
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.19 
			</td>
		

			<td valign='top'>
				46.1 
			</td>
		

			<td valign='top'>
				Agents involved in the sale of a variety of goods 
			</td>
		

			<td valign='top'>
				4610 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale trade in own name, see 46.2 to 46.9- retail sale by non-store commission agents, see 47.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399009
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.2 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Wholesale of agricultural raw materials and live animals 
			</td>
		

			<td valign='top'>
				462 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399010
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.21 
			</td>
		

			<td valign='top'>
				46.2 
			</td>
		

			<td valign='top'>
				Wholesale of grain, unmanufactured tobacco, seeds and animal feeds 
			</td>
		

			<td valign='top'>
				4620 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of grains and seeds- wholesale of oleaginous fruits- wholesale of unmanufactured tobacco- wholesale of animal feeds and agricultural raw material n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of textile fibres, see 46.76 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399011
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.22 
			</td>
		

			<td valign='top'>
				46.2 
			</td>
		

			<td valign='top'>
				Wholesale of flowers and plants 
			</td>
		

			<td valign='top'>
				4620 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of flowers, plants and bulbs 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399012
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.23 
			</td>
		

			<td valign='top'>
				46.2 
			</td>
		

			<td valign='top'>
				Wholesale of live animals 
			</td>
		

			<td valign='top'>
				4620 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399013
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.24 
			</td>
		

			<td valign='top'>
				46.2 
			</td>
		

			<td valign='top'>
				Wholesale of hides, skins and leather 
			</td>
		

			<td valign='top'>
				4620 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399014
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Wholesale of food, beverages and tobacco 
			</td>
		

			<td valign='top'>
				463 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399015
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.31 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of fruit and vegetables 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of fresh fruits and vegetables- wholesale of preserved fruits and vegetables 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399016
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.32 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of meat and meat products 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399017
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.33 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of dairy products, eggs and edible oils and fats 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of dairy products- wholesale of eggs and egg products- wholesale of edible oils and fats of animal or vegetable origin 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399018
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.34 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of beverages 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of alcoholic beverages- wholesale of non-alcoholic beverages 
			</td>
		

			<td valign='top'>
				This class also includes:- buying of wine in bulk and bottling without transformation 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- blending of wine or distilled spirits, see 11.01, 11.02 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399019
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.35 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of tobacco products 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399020
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.36 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of sugar and chocolate and sugar confectionery 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of sugar, chocolate and sugar confectionery- wholesale of bakery products 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Buying granulated sugar from producers and transforming that sugar into cube sugar 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399021
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.37 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of coffee, tea, cocoa and spices 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399022
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.38 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Wholesale of other food, including fish, crustaceans and molluscs 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- wholesale of feed for pet animals 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399023
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.39 
			</td>
		

			<td valign='top'>
				46.3 
			</td>
		

			<td valign='top'>
				Non-specialised wholesale of food, beverages and tobacco 
			</td>
		

			<td valign='top'>
				4630 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399024
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Wholesale of household goods 
			</td>
		

			<td valign='top'>
				464 
			</td>
		

			<td valign='top'>
				This group includes the wholesale of household goods, including textiles. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399025
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.41 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of textiles 
			</td>
		

			<td valign='top'>
				4641 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of yarn- wholesale of fabrics- wholesale of household linen etc.- wholesale of haberdashery: needles, sewing thread etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of textile fibres, see 46.76 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399026
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.42 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of clothing and footwear 
			</td>
		

			<td valign='top'>
				4641 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of clothing, including sports clothes- wholesale of clothing accessories such as gloves, ties and braces- wholesale of footwear- wholesale of fur articles- wholesale of umbrellas 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of jewellery, see 46.48- wholesale of leather goods, see 46.49- wholesale of special sports equipment footwear such as ski boots, see 46.49 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399027
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.43 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of electrical household appliances 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of electrical household appliances- wholesale of radio and television equipment- wholesale of photographic and optical goods- wholesale of electrical heating appliances- wholesale of recorded audio and video tapes, CDs, DVDs 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of blank audio and video tapes, CDs, DVDs, see 46.52- wholesale of sewing machines, see 46.64 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399028
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.44 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of china and glassware and cleaning materials 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of china and glassware- wholesale of cleaning materials 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399029
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.45 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of perfume and cosmetics 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of perfumeries, cosmetics and soaps 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399030
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.46 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of pharmaceutical goods 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of pharmaceutical and medical goods 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399031
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.47 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of furniture, carpets and lighting equipment 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of household furniture- wholesale of carpets- wholesale of lighting equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of office furniture, see 46.65 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399032
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.48 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of watches and jewellery 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399033
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.49 
			</td>
		

			<td valign='top'>
				46.4 
			</td>
		

			<td valign='top'>
				Wholesale of other household goods 
			</td>
		

			<td valign='top'>
				4649 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of woodenware, wickerwork and corkware etc.- wholesale of bicycles and their parts and accessories- wholesale of stationery, books, magazines and newspapers- wholesale of leather goods and travel accessories- wholesale of musical instruments- wholesale of games and toys- wholesale of sports goods, including special sports footwear such as ski boots 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Wholesale of medals and sports cups 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399034
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.5 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Wholesale of information and communication equipment 
			</td>
		

			<td valign='top'>
				465 
			</td>
		

			<td valign='top'>
				This group includes the wholesale of information and communications technology (ICT) equipment, i.e. computers, telecommunications equipment and parts. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399035
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.51 
			</td>
		

			<td valign='top'>
				46.5 
			</td>
		

			<td valign='top'>
				Wholesale of computers, computer peripheral equipment and software 
			</td>
		

			<td valign='top'>
				4651 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of computers and computer peripheral equipment- wholesale of software 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Wholesale of smart boards 
			</td>
		

			<td valign='top'>
				This class excludes:- wholesale of electronic parts, see 46.52- wholesale of office machinery and equipment, (except computers and peripheral equipment), see 46.66 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399036
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.52 
			</td>
		

			<td valign='top'>
				46.5 
			</td>
		

			<td valign='top'>
				Wholesale of electronic and telecommunications equipment and parts 
			</td>
		

			<td valign='top'>
				4652 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of electronic valves and tubes- wholesale of semi-conductor devices- wholesale of microchips and integrated circuits- wholesale of printed circuits- wholesale of blank audio and video tapes and diskettes, magnetic and optical disks (CDs, DVDs)- wholesale of telephone and communications equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of recorded audio and video tapes, CDs, DVDs, see 46.43- wholesale of computers and computer peripheral equipment, see 46.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399037
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Wholesale of other machinery, equipment and supplies 
			</td>
		

			<td valign='top'>
				466 
			</td>
		

			<td valign='top'>
				This group includes the wholesale of specialised machinery, equipment and supplies for all kinds of industries and general purpose machinery. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399038
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.61 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of agricultural machinery, equipment and supplies 
			</td>
		

			<td valign='top'>
				4653 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of agricultural machinery and equipment:  • ploughs, manure spreaders, seeders  • harvesters  • threshers  • milking machines  • poultry-keeping machines, bee-keeping machines  • tractors used in agriculture and forestry 
			</td>
		

			<td valign='top'>
				This class also includes:- lawn mowers however operated 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399039
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.62 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of machine tools 
			</td>
		

			<td valign='top'>
				4659 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of machine tools of any type and for any material 
			</td>
		

			<td valign='top'>
				This class also includes:- wholesale of computer-controlled machine tools 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399040
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.63 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of mining, construction and civil engineering machinery 
			</td>
		

			<td valign='top'>
				4659 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399041
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.64 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of machinery for the textile industry and of sewing and knitting machines 
			</td>
		

			<td valign='top'>
				4659 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- wholesale of computer-controlled machinery for the textile industry and of computer-controlled sewing and knitting machines 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399042
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.65 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of office furniture 
			</td>
		

			<td valign='top'>
				4659 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale trade services related to:  • goods classified in 31.01 (Manufacture of office and shop furniture) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399043
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.66 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of other office machinery and equipment 
			</td>
		

			<td valign='top'>
				4659 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of office machinery and equipment, except computers and computer peripheral equipment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of computers and peripheral equipment, see 46.51- wholesale of electronic parts and telephone and communications equipment, see 46.52 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399044
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.69 
			</td>
		

			<td valign='top'>
				46.6 
			</td>
		

			<td valign='top'>
				Wholesale of other machinery and equipment 
			</td>
		

			<td valign='top'>
				4659 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of transport equipment except motor vehicles, motorcycles and bicycles- wholesale of production-line robots- wholesale of wires and switches and other installation equipment for industrial use- wholesale of other electrical material such as electrical motors, transformers- wholesale of other machinery n.e.c. for use in industry (except mining, construction, civil engineering and textile industry), trade and navigation and other services 
			</td>
		

			<td valign='top'>
				This class also includes:- wholesale of measuring instruments and equipment 
			</td>
		

			<td valign='top'>
				- Wholesale of equipment for fish farming 
			</td>
		

			<td valign='top'>
				This class excludes:- wholesale of motor vehicles, trailers and caravans, see 45.1- wholesale of motor vehicle parts, see 45.31- wholesale of motorcycles, see 45.40- wholesale of bicycles, see 46.49 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399045
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Other specialised wholesale 
			</td>
		

			<td valign='top'>
				466 
			</td>
		

			<td valign='top'>
				This group includes other specialised wholesale activities not classified in other groups of this division. This includes the wholesale of intermediate products, except agricultural, typically not for household use. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399046
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.71 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of solid, liquid and gaseous fuels and related products 
			</td>
		

			<td valign='top'>
				4661 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of fuels, greases, lubricants, oils such as:  • charcoal, coal, coke, fuel wood, naphtha  • crude petroleum, crude oil, diesel fuel, gasoline, fuel oil, heating oil, kerosene  • liquefied petroleum gases, butane and propane gas  • lubricating oils and greases, refined petroleum products 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399047
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.72 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of metals and metal ores 
			</td>
		

			<td valign='top'>
				4662 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of ferrous and non-ferrous metal ores- wholesale of ferrous and non-ferrous metals in primary forms- wholesale of ferrous and non-ferrous semi-finished metal products n.e.c.- wholesale of gold and other precious metals 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Cutting of steel sheets on own account, with subsequent sale 
			</td>
		

			<td valign='top'>
				This class excludes:- wholesale of metal scrap, see 46.77 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399048
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.73 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of wood, construction materials and sanitary equipment 
			</td>
		

			<td valign='top'>
				4663 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of wood in the rough- wholesale of products of primary processing of wood- wholesale of paint and varnish- wholesale of construction materials:  • sand, gravel- wholesale of wallpaper and floor coverings- wholesale of flat glass- wholesale of sanitary equipment:  • baths, washbasins, toilets and other sanitary porcelain- wholesale of prefabricated buildings 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399049
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.74 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of hardware, plumbing and heating equipment and supplies 
			</td>
		

			<td valign='top'>
				4663 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of hardware and locks- wholesale of fittings and fixtures- wholesale of hot water heaters- wholesale of sanitary installation equipment:  • tubes, pipes, fittings, taps, T-pieces, connections, rubber pipes etc.- wholesale of tools such as hammers, saws, screwdrivers and other hand tools 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399050
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.75 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of chemical products 
			</td>
		

			<td valign='top'>
				4669 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of industrial chemicals:  • aniline, printing ink, essential oils, industrial gases, chemical glues, colouring matter, synthetic resin, methanol, paraffin, scents and flavourings, soda, industrial salt, acids and sulphurs, starch derivates etc.- wholesale of fertilisers and agrochemical products 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399051
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.76 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of other intermediate products 
			</td>
		

			<td valign='top'>
				4669 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of plastic materials in primary forms- wholesale of rubber- wholesale of textile fibres etc.- wholesale of paper in bulk- wholesale of precious stones 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399052
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.77 
			</td>
		

			<td valign='top'>
				46.7 
			</td>
		

			<td valign='top'>
				Wholesale of waste and scrap 
			</td>
		

			<td valign='top'>
				4669 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of metal and non-metal waste and scrap and materials for recycling, including collecting, sorting, separating, stripping of used goods such as cars in order to obtain reusable parts, packing and repacking, storage and delivery, but without a real transformation process. Additionally, the purchased and sold waste has a remaining value. 
			</td>
		

			<td valign='top'>
				This class also includes:- dismantling of automobiles, computers, televisions and other equipment to obtain and re-sell usable parts 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- collection of household and industrial waste, see 38.1- treatment of waste, not for a further use in an industrial manufacturing process, but with the aim of disposal, see 38.2- processing of waste and scrap and other articles into secondary raw material when a real transformation process is required (the resulting secondary raw material is fit for direct use in an industrial manufacturing process, but is not a final product), see 38.3- dismantling of automobiles, computers, televisions and other equipment for materials recovery, see 38.31- ship-breaking, see 38.31- shredding of cars by means of a mechanical process, see 38.32- retail sale of second-hand goods, see 47.79 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399053
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				46.9 
			</td>
		

			<td valign='top'>
				46 
			</td>
		

			<td valign='top'>
				Non-specialised wholesale trade 
			</td>
		

			<td valign='top'>
				469 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399054
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				46.90 
			</td>
		

			<td valign='top'>
				46.9 
			</td>
		

			<td valign='top'>
				Non-specialised wholesale trade 
			</td>
		

			<td valign='top'>
				4690 
			</td>
		

			<td valign='top'>
				This class includes:- wholesale of a variety of goods without any particular specialisation 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399055
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				G 
			</td>
		

			<td valign='top'>
				Retail trade, except of motor vehicles and motorcycles 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				This division includes the resale (sale without transformation) of new and used goods mainly to the general public for personal or household consumption or utilisation, by shops, department stores, stalls, mail-order houses, door-to-door sales persons, hawkers, consumer cooperatives etc.Retail trade is classified first by type of sale outlet (retail trade in stores: groups 47.1 to 47.7; retail trade not in stores: groups 47.8 and 47.9). Retail trade in stores includes the retail sale of used goods (class 47.79). For retail sale in stores, there exists a further distinction between specialised retail sale (groups 47.2 to 47.7) and non-specialised retail sale (group 47.1). The above groups are further subdivided by the range of products sold. Sale not via stores is subdivided according to the forms of trade, such as retail sale via stalls and markets (group 47.8) and other non-store retail sale, e.g. mail order, door-to-door, by vending machines etc. (group 47.9).The goods sold in this division are limited to goods usually referred to as consumer goods or retail goods. Therefore goods not normally entering the retail trade, such as cereal grains, ores, industrial machinery etc. are excluded. 
			</td>
		

			<td valign='top'>
				This division also includes units engaged primarily in selling to the general public, from displayed merchandise, products such as personal computers, stationery, paint or timber, although these products may not be for personal or household use. Handling that is customary in trade does not affect the basic character of the merchandise and may include, for example, sorting, separating, mixing and packaging.This division also includes the retail sale by commission agents and activities of retail auctioning houses. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes:- sale of farmers' products by farmers, see division 01- manufacture and sale of goods, which is generally classified as manufacturing in divisions 10-32- sale of motor vehicles, motorcycles and their parts, see division 45- trade in cereal grains, ores, crude petroleum, industrial chemicals, iron and steel and industrial machinery and equipment, see division 46- sale of food and drinks for consumption on the premises and sale of takeaway food, see division 56- rental of personal and household goods to the general public, see group 77.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399056
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.1 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale in non-specialised stores 
			</td>
		

			<td valign='top'>
				471 
			</td>
		

			<td valign='top'>
				This group includes the retail sale of a variety of product lines in the same unit (non-specialised stores), such as supermarkets or department stores. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399057
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.11 
			</td>
		

			<td valign='top'>
				47.1 
			</td>
		

			<td valign='top'>
				Retail sale in non-specialised stores with food, beverages or tobacco predominating 
			</td>
		

			<td valign='top'>
				4711 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of a large variety of goods of which, however, food products, beverages or tobacco should be predominant:  • activities of general stores that have, apart from their main sales of food products, beverages or tobacco, several other lines of merchandise such as wearing apparel, furniture, appliances, hardware, cosmetics etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399058
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.19 
			</td>
		

			<td valign='top'>
				47.1 
			</td>
		

			<td valign='top'>
				Other retail sale in non-specialised stores 
			</td>
		

			<td valign='top'>
				4719 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of a large variety of goods of which food products, beverages or tobacco are not predominant- activities of department stores carrying a general line of merchandise, including wearing apparel, furniture, appliances, hardware, cosmetics, jewellery, toys, sports goods etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399059
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale of food, beverages and tobacco in specialised stores 
			</td>
		

			<td valign='top'>
				472 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399060
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.21 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Retail sale of fruit and vegetables in specialised stores 
			</td>
		

			<td valign='top'>
				4721 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of fresh fruit and vegetables- retail sale of prepared and preserved fruits and vegetables 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399061
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.22 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Retail sale of meat and meat products in specialised stores 
			</td>
		

			<td valign='top'>
				4721 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of meat and meat products (including poultry) 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399062
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.23 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Retail sale of fish, crustaceans and molluscs in specialised stores 
			</td>
		

			<td valign='top'>
				4721 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of fish, other seafood and products thereof 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399063
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.24 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Retail sale of bread, cakes, flour confectionery and sugar confectionery in specialised stores 
			</td>
		

			<td valign='top'>
				4721 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				- Baking of pre-baked bread and rolls 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399064
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.25 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Retail sale of beverages in specialised stores 
			</td>
		

			<td valign='top'>
				4722 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of beverages (not for consumption on the premises):  • alcoholic beverages  • non-alcoholic beverages 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399065
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.26 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Retail sale of tobacco products in specialised stores 
			</td>
		

			<td valign='top'>
				4723 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of tobacco- retail sale of tobacco products 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399066
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.29 
			</td>
		

			<td valign='top'>
				47.2 
			</td>
		

			<td valign='top'>
				Other retail sale of food in specialised stores 
			</td>
		

			<td valign='top'>
				4721 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of dairy products and eggs- retail sale of other food products n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399067
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.3 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale of automotive fuel in specialised stores 
			</td>
		

			<td valign='top'>
				473 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399068
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.30 
			</td>
		

			<td valign='top'>
				47.3 
			</td>
		

			<td valign='top'>
				Retail sale of automotive fuel in specialised stores 
			</td>
		

			<td valign='top'>
				4730 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of fuel for motor vehicles and motorcycles 
			</td>
		

			<td valign='top'>
				This class also includes:- retail sale of lubricating products and cooling products for motor vehicles 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of fuels, see 46.71- retail sale of liquefied petroleum gas for cooking or heating, see 47.78 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399069
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.4 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale of information and communication equipment in specialised stores 
			</td>
		

			<td valign='top'>
				474 
			</td>
		

			<td valign='top'>
				This group includes the retail sale of information and communications technology (ICT) equipment, such as computers and peripheral equipment, telecommunications equipment and consumer electronics, by specialised stores. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399070
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.41 
			</td>
		

			<td valign='top'>
				47.4 
			</td>
		

			<td valign='top'>
				Retail sale of computers, peripheral units and software in specialised stores 
			</td>
		

			<td valign='top'>
				4741 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of computers- retail sale of computer peripheral equipment- retail sale of video game consoles- retail sale of non-customised software, including video games 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of blank tapes and disks, see 47.63 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399071
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.42 
			</td>
		

			<td valign='top'>
				47.4 
			</td>
		

			<td valign='top'>
				Retail sale of telecommunications equipment in specialised stores 
			</td>
		

			<td valign='top'>
				4741 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399072
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.43 
			</td>
		

			<td valign='top'>
				47.4 
			</td>
		

			<td valign='top'>
				Retail sale of audio and video equipment in specialised stores 
			</td>
		

			<td valign='top'>
				4742 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of radio and television equipment- retail sale of audio and video equipment- retail sale of CD, DVD etc. players and recorders 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399073
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.5 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale of other household equipment in specialised stores 
			</td>
		

			<td valign='top'>
				475 
			</td>
		

			<td valign='top'>
				This group includes the retail sale of household equipment, such as textiles, hardware, carpets, electrical appliances or furniture, in specialised stores. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399074
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.51 
			</td>
		

			<td valign='top'>
				47.5 
			</td>
		

			<td valign='top'>
				Retail sale of textiles in specialised stores 
			</td>
		

			<td valign='top'>
				4751 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of fabrics- retail sale of knitting yarn- retail sale of basic materials for rug, tapestry or embroidery making- retail sale of textiles- retail sale of haberdashery: needles, sewing thread etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of clothing, see 47.71 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399075
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.52 
			</td>
		

			<td valign='top'>
				47.5 
			</td>
		

			<td valign='top'>
				Retail sale of hardware, paints and glass in specialised stores 
			</td>
		

			<td valign='top'>
				4752 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of hardware- retail sale of paints, varnishes and lacquers- retail sale of flat glass- retail sale of other building material such as bricks, wood, sanitary equipment- retail sale of do-it-yourself material and equipment 
			</td>
		

			<td valign='top'>
				This class also includes:- retail sale of lawnmowers, however operated- retail sale of saunas 
			</td>
		

			<td valign='top'>
				- Retail sale of non-electric solar collectors 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399076
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.53 
			</td>
		

			<td valign='top'>
				47.5 
			</td>
		

			<td valign='top'>
				Retail sale of carpets, rugs, wall and floor coverings in specialised stores 
			</td>
		

			<td valign='top'>
				4753 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of carpets and rugs- retail sale of curtains and net curtains- retail sale of wallpaper and floor coverings 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of cork floor tiles, see 47.52 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399077
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.54 
			</td>
		

			<td valign='top'>
				47.5 
			</td>
		

			<td valign='top'>
				Retail sale of electrical household appliances in specialised stores 
			</td>
		

			<td valign='top'>
				4759 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of audio and video equipment, see 47.43 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399078
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.59 
			</td>
		

			<td valign='top'>
				47.5 
			</td>
		

			<td valign='top'>
				Retail sale of furniture, lighting equipment and other household articles in specialised stores 
			</td>
		

			<td valign='top'>
				4759 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of household furniture- retail sale of articles for lighting- retail sale of household utensils and cutlery, crockery, glassware, china and pottery- retail sale of wooden, cork and wickerwork goods- retail sale of non-electrical household appliances- retail sale of musical instruments and scores- retail sale of electrical security alarm systems, such as locking devices, safes, and vaults, without installation or maintenance services- retail sale of household articles and equipment n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of antiques, see 47.79 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399079
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.6 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale of cultural and recreation goods in specialised stores 
			</td>
		

			<td valign='top'>
				476 
			</td>
		

			<td valign='top'>
				This group includes the retail sale in specialised stores of cultural and recreation goods, such as books, newspapers, music and video recordings, sporting equipment, games and toys. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399080
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.61 
			</td>
		

			<td valign='top'>
				47.6 
			</td>
		

			<td valign='top'>
				Retail sale of books in specialised stores 
			</td>
		

			<td valign='top'>
				4761 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of books of all kinds 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of second-hand or antique books, see 47.79 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399081
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.62 
			</td>
		

			<td valign='top'>
				47.6 
			</td>
		

			<td valign='top'>
				Retail sale of newspapers and stationery in specialised stores 
			</td>
		

			<td valign='top'>
				4761 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- retail sale of office supplies such as pens, pencils, paper etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399082
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.63 
			</td>
		

			<td valign='top'>
				47.6 
			</td>
		

			<td valign='top'>
				Retail sale of music and video recordings in specialised stores 
			</td>
		

			<td valign='top'>
				4762 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of musical records, audio tapes, compact discs and cassettes- retail sale of video tapes and DVDs 
			</td>
		

			<td valign='top'>
				This class also includes:- retail sale of blank tapes and discs 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399083
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.64 
			</td>
		

			<td valign='top'>
				47.6 
			</td>
		

			<td valign='top'>
				Retail sale of sporting equipment in specialised stores 
			</td>
		

			<td valign='top'>
				4763 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of sports goods, fishing gear, camping goods, boats and bicycles 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399084
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.65 
			</td>
		

			<td valign='top'>
				47.6 
			</td>
		

			<td valign='top'>
				Retail sale of games and toys in specialised stores 
			</td>
		

			<td valign='top'>
				4764 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of games and toys, made of all materials 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of video game consoles, see 47.41- retail sale of non-customised software, including video games, see 47.41 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399085
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale of other goods in specialised stores 
			</td>
		

			<td valign='top'>
				477 
			</td>
		

			<td valign='top'>
				This group includes the sale in specialised stores carrying a particular line of products not included in other parts of the classification, such as clothing, footwear and leather articles, pharmaceutical and medical goods, watches, souvenirs, cleaning materials, weapons, flowers and pets and others. 
			</td>
		

			<td valign='top'>
				Also included is the retail sale of used goods in specialised stores. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399086
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.71 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of clothing in specialised stores 
			</td>
		

			<td valign='top'>
				4771 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of articles of clothing- retail sale of articles of fur- retail sale of clothing accessories such as gloves, ties, braces etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of textiles, see 47.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399087
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.72 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of footwear and leather goods in specialised stores 
			</td>
		

			<td valign='top'>
				4771 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of footwear- retail sale of leather goods- retail sale of travel accessories of leather and leather substitutes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of special sports equipment footwear such as ski boots, see 47.64 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399088
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.73 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Dispensing chemist in specialised stores 
			</td>
		

			<td valign='top'>
				4772 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of pharmaceuticals 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399089
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.74 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of medical and orthopaedic goods in specialised stores 
			</td>
		

			<td valign='top'>
				4772 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399090
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.75 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of cosmetic and toilet articles in specialised stores 
			</td>
		

			<td valign='top'>
				4772 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of perfumery, cosmetic and toilet articles 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399091
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.76 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of flowers, plants, seeds, fertilisers, pet animals and pet food in specialised stores 
			</td>
		

			<td valign='top'>
				4773 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399092
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.77 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of watches and jewellery in specialised stores 
			</td>
		

			<td valign='top'>
				4773 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399093
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.78 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Other retail sale of new goods in specialised stores 
			</td>
		

			<td valign='top'>
				4773 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of photographic, optical and precision equipment- activities of opticians- retail sale of souvenirs, craftwork and religious articles- activities of commercial art galleries- retail sale of household fuel oil, bottled gas, coal and fuel wood- retail sale of weapons and ammunition- retail sale of stamps and coins- retail trade services of commercial art galleries- retail sale of non-food products n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399094
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.79 
			</td>
		

			<td valign='top'>
				47.7 
			</td>
		

			<td valign='top'>
				Retail sale of second-hand goods in stores 
			</td>
		

			<td valign='top'>
				4774 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of second-hand books- retail sale of other second-hand goods- retail sale of antiques- activities of auctioning houses (retail) 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of second-hand motor vehicles, see 45.1- activities of Internet auctions and other non-store auctions (retail), see 47.91, 47.99- activities of pawn shops, see 64.92 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399095
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.8 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail sale via stalls and markets 
			</td>
		

			<td valign='top'>
				478 
			</td>
		

			<td valign='top'>
				This group includes the retail sale of any kind of new or second-hand product in a usually movable stall either along a public road or at a fixed marketplace. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399096
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.81 
			</td>
		

			<td valign='top'>
				47.8 
			</td>
		

			<td valign='top'>
				Retail sale via stalls and markets of food, beverages and tobacco products 
			</td>
		

			<td valign='top'>
				4781 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- retail sale of prepared food for immediate consumption (mobile food vendors), see 56.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399097
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.82 
			</td>
		

			<td valign='top'>
				47.8 
			</td>
		

			<td valign='top'>
				Retail sale via stalls and markets of textiles, clothing and footwear 
			</td>
		

			<td valign='top'>
				4782 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399098
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.89 
			</td>
		

			<td valign='top'>
				47.8 
			</td>
		

			<td valign='top'>
				Retail sale via stalls and markets of other goods 
			</td>
		

			<td valign='top'>
				4789 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of other goods via stalls or markets, such as:  • carpets and rugs  • books  • games and toys  • household appliances and consumer electronics  • music and video recordings 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399099
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				47.9 
			</td>
		

			<td valign='top'>
				47 
			</td>
		

			<td valign='top'>
				Retail trade not in stores, stalls or markets 
			</td>
		

			<td valign='top'>
				479 
			</td>
		

			<td valign='top'>
				This group includes retail sale activities by mail order houses, over the Internet, through door-to-door sales, vending machines etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399100
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.91 
			</td>
		

			<td valign='top'>
				47.9 
			</td>
		

			<td valign='top'>
				Retail sale via mail order houses or via Internet 
			</td>
		

			<td valign='top'>
				4791 
			</td>
		

			<td valign='top'>
				This class includes retail sale activities via mail order houses or via Internet, i.e. retail sale activities where the buyer makes his choice on the basis of advertisements, catalogues, information provided on a website, models or any other means of advertising and places his order by mail, phone or over the Internet (usually through special means provided by a website). The products purchased can be either directly downloaded from the Internet or physically delivered to the customer.This class includes:- retail sale of any kind of product by mail order - retail sale of any kind of product over the Internet 
			</td>
		

			<td valign='top'>
				This class also includes:- direct sale via television, radio and telephone- Internet retail auctions 
			</td>
		

			<td valign='top'>
				- Recipes web site publishing, including sale of ingredients necessary and home delivery of ingredients 
			</td>
		

			<td valign='top'>
				This class excludes:- retail sale of motor vehicles and motor vehicles parts and accessories over the Internet, see groups 45.1, 45.3- retail sale of motorcycles and motorcycles parts and accessories over the Internet, see 45.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399101
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				47.99 
			</td>
		

			<td valign='top'>
				47.9 
			</td>
		

			<td valign='top'>
				Other retail sale not in stores, stalls or markets 
			</td>
		

			<td valign='top'>
				4799 
			</td>
		

			<td valign='top'>
				This class includes:- retail sale of any kind of product in any way that is not included in previous classes:  • by direct sales or door-to-door sales persons  • through vending machines etc.- direct selling of fuel (heating oil, firewood, etc.), delivered to the customers premises- activities of non-store auctions (retail, except Internet)- retail sale by (non-store) commission agents 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Retail sale via vending machines- Retail sale via coffee dispensers 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399102
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				H 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				TRANSPORTATION AND STORAGE 
			</td>
		

			<td valign='top'>
				H 
			</td>
		

			<td valign='top'>
				This section includes the provision of passenger or freight transport, whether scheduled or not, by rail, pipeline, road, water or air and associated activities such as terminal and parking facilities, cargo handling, storage etc. Included in this section is the rental of transport equipment with driver or operator. Also included are postal and courier activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This section excludes:- major repair or alteration of transport equipment, except motor vehicles, see group 33.1- construction, maintenance and repair of roads, railways, harbours, airfields, see division 42- maintenance and repair of motor vehicles, see 45.20- rental of transport equipment without driver or operator, see 77.1, 77.3 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399103
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				H 
			</td>
		

			<td valign='top'>
				Land transport and transport via pipelines 
			</td>
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				This division includes the transport of passengers and freight via road and rail, as well as freight transport via pipelines. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399104
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				49.1 
			</td>
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				Passenger rail transport, interurban 
			</td>
		

			<td valign='top'>
				491 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399105
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.10 
			</td>
		

			<td valign='top'>
				49.1 
			</td>
		

			<td valign='top'>
				Passenger rail transport, interurban 
			</td>
		

			<td valign='top'>
				4911 
			</td>
		

			<td valign='top'>
				This class includes:- rail transportation of passengers using railroad rolling stock on mainline networks, spread over an extensive geographic area- passenger transport by interurban railways - operation of sleeping cars or dining cars as an integrated operation of railway companies 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- passenger transport by urban and suburban transit systems, see 49.31- passenger terminal activities, see 52.21- operation of railroad infrastructure; related activities such as switching and shunting, see 52.21- operation of sleeping cars or dining cars when operated by separate units, see 55.90, 56.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399106
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				49.2 
			</td>
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				Freight rail transport 
			</td>
		

			<td valign='top'>
				491 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399107
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.20 
			</td>
		

			<td valign='top'>
				49.2 
			</td>
		

			<td valign='top'>
				Freight rail transport 
			</td>
		

			<td valign='top'>
				4912 
			</td>
		

			<td valign='top'>
				This class includes:- freight transport on mainline rail networks as well as short line freight railroads 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- warehousing and storage, see 52.10- freight terminal activities, see 52.21- operation of railroad infrastructure; related activities such as switching and shunting, see 52.21- cargo handling, see 52.24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399108
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				49.3 
			</td>
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				Other passenger land transport 
			</td>
		

			<td valign='top'>
				492 
			</td>
		

			<td valign='top'>
				This group includes all land-based passenger transport activities other than rail transport. However, rail transport as part of urban or suburban transport systems is included there. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399109
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.31 
			</td>
		

			<td valign='top'>
				49.3 
			</td>
		

			<td valign='top'>
				Urban and suburban passenger land transport 
			</td>
		

			<td valign='top'>
				4921 
			</td>
		

			<td valign='top'>
				This class includes:- land transport of passengers by urban or suburban transport systems. This may include different modes of land transport, such as by motor bus, tramway, streetcar, trolley bus, underground and elevated railways etc. The transport is carried out on scheduled routes normally following a fixed time schedule, entailing the picking up and setting down of passengers at normally fixed stops. 
			</td>
		

			<td valign='top'>
				This class also includes:- town-to-airport or town-to-station lines- operation of funicular railways, aerial cableways etc. if part of urban or suburban transit systems 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- passenger transport by interurban railways, see 49.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399110
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.32 
			</td>
		

			<td valign='top'>
				49.3 
			</td>
		

			<td valign='top'>
				Taxi operation 
			</td>
		

			<td valign='top'>
				4922 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- other rental of private cars with driver 
			</td>
		

			<td valign='top'>
				- Motorbike-taxi services- Taxi-radio services 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399111
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.39 
			</td>
		

			<td valign='top'>
				49.3 
			</td>
		

			<td valign='top'>
				Other passenger land transport n.e.c. 
			</td>
		

			<td valign='top'>
				4922 
			</td>
		

			<td valign='top'>
				This class includes:- other passenger road transport:  • scheduled long-distance bus services  • charters, excursions and other occasional coach services  • airport shuttles- operation of teleferics, funiculars, ski and cable lifts if not part of urban or suburban transit systems 
			</td>
		

			<td valign='top'>
				This class also includes:- operation of school buses and buses for transport of employees- passenger transport by man- or animal-drawn vehicles 
			</td>
		

			<td valign='top'>
				- Bike-taxi services 
			</td>
		

			<td valign='top'>
				This class excludes:- ambulance transport, see 86.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399112
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				49.4 
			</td>
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				Freight transport by road and removal services 
			</td>
		

			<td valign='top'>
				492 
			</td>
		

			<td valign='top'>
				This group includes all land-based freight transport activities other than rail transport. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399113
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.41 
			</td>
		

			<td valign='top'>
				49.4 
			</td>
		

			<td valign='top'>
				Freight transport by road 
			</td>
		

			<td valign='top'>
				4923 
			</td>
		

			<td valign='top'>
				This class includes:- all freight transport operations by road:  • logging haulage  • stock haulage  • refrigerated haulage  • heavy haulage  • bulk haulage, including haulage in tanker trucks including milk collection at farms  • haulage of automobiles  • transport of waste and waste materials, without collection or disposal 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of trucks with driver- freight transport by man or animal-drawn vehicles 
			</td>
		

			<td valign='top'>
				- Concrete mixer lorry rental, with operator- Road transport of letters as freight, carried out by a third party on behalf of postal or courier units- Transportation or moving objects within an industrial plant, on a fee or contract basis 
			</td>
		

			<td valign='top'>
				This class excludes:- log hauling within the forest, as part of logging operations, see 02.40- distribution of water by trucks, see 36.00- operation of terminal facilities for handling freight, see 52.21- crating and packing activities for transport, see 52.29- post and courier activities, see 53.10, 53.20- waste transport as integrated part of waste collection activities, see 38.11, 38.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399114
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.42 
			</td>
		

			<td valign='top'>
				49.4 
			</td>
		

			<td valign='top'>
				Removal services 
			</td>
		

			<td valign='top'>
				4923 
			</td>
		

			<td valign='top'>
				This class includes:- removal (relocation) services to businesses and households by road transport 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399115
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				49.5 
			</td>
		

			<td valign='top'>
				49 
			</td>
		

			<td valign='top'>
				Transport via pipeline 
			</td>
		

			<td valign='top'>
				493 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399116
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				49.50 
			</td>
		

			<td valign='top'>
				49.5 
			</td>
		

			<td valign='top'>
				Transport via pipeline 
			</td>
		

			<td valign='top'>
				4930 
			</td>
		

			<td valign='top'>
				This class includes:- transport of gases, liquids, water, slurry and other commodities via pipelines 
			</td>
		

			<td valign='top'>
				This class also includes:- operation of pump stations 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- distribution of natural or manufactured gas, steam or water, see 35.22, 35.30, 36.00- transport of liquids by trucks, see 49.41 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399117
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				50 
			</td>
		

			<td valign='top'>
				H 
			</td>
		

			<td valign='top'>
				Water transport 
			</td>
		

			<td valign='top'>
				50 
			</td>
		

			<td valign='top'>
				This division includes the transport of passengers or freight over water, whether scheduled or not. Also included are the operation of towing or pushing boats, excursion, cruise or sightseeing boats, ferries, water taxis etc. Although the location is an indicator for the separation between sea and inland water transport, the deciding factor is the type of vessel used. Transport on sea-going vessels is classified in groups 50.1 and 50.2, while transport using other vessels is classified in groups 50.3 and 50.4. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes restaurant and bar activities on board ships (see 56.10, 56.30), if carried out by separate units. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399118
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				50.1 
			</td>
		

			<td valign='top'>
				50 
			</td>
		

			<td valign='top'>
				Sea and coastal passenger water transport 
			</td>
		

			<td valign='top'>
				501 
			</td>
		

			<td valign='top'>
				This group includes the transport of passengers on vessels designed for operating on sea or coastal waters. 
			</td>
		

			<td valign='top'>
				Also included is the transport of passengers on great lakes etc. when similar types of vessels are used. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399119
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				50.10 
			</td>
		

			<td valign='top'>
				50.1 
			</td>
		

			<td valign='top'>
				Sea and coastal passenger water transport 
			</td>
		

			<td valign='top'>
				5011 
			</td>
		

			<td valign='top'>
				This class includes:- transport of passengers over seas and coastal waters, whether scheduled or not:  • operation of excursion, cruise or sightseeing boats  • operation of ferries, water taxis etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of pleasure boats with crew for sea and coastal water transport (e.g. for fishing cruises) 
			</td>
		

			<td valign='top'>
				- Ferry transport of cars with driver 
			</td>
		

			<td valign='top'>
				This class excludes:- restaurant and bar activities on board ships, when provided by separate units, see 56.10, 56.30- rental of pleasure boats and yachts without crew, see 77.21- rental of commercial ships or boats without crew, see 77.34- operation of &quot;floating casinos”, see 92.00 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399120
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				50.2 
			</td>
		

			<td valign='top'>
				50 
			</td>
		

			<td valign='top'>
				Sea and coastal freight water transport 
			</td>
		

			<td valign='top'>
				501 
			</td>
		

			<td valign='top'>
				This group includes the transport of freight on vessels designed for operating on sea or coastal waters. 
			</td>
		

			<td valign='top'>
				Also included is the transport of freight on great lakes etc. when similar types of vessels are used. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399121
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				50.20 
			</td>
		

			<td valign='top'>
				50.2 
			</td>
		

			<td valign='top'>
				Sea and coastal freight water transport 
			</td>
		

			<td valign='top'>
				5012 
			</td>
		

			<td valign='top'>
				This class includes:- transport of freight over seas and coastal waters, whether scheduled or not- transport by towing or pushing of barges, oil rigs etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of vessels with crew for sea and coastal freight water transport 
			</td>
		

			<td valign='top'>
				- Ferry transport of cars without driver 
			</td>
		

			<td valign='top'>
				This class excludes:- storage of freight, see 52.10- harbour operation and other auxiliary activities such as docking, pilotage, lighterage, vessel salvage, see 52.22- cargo handling, see 52.24- rental of commercial ships or boats without crew, see 77.34 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399122
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				50.3 
			</td>
		

			<td valign='top'>
				50 
			</td>
		

			<td valign='top'>
				Inland passenger water transport 
			</td>
		

			<td valign='top'>
				502 
			</td>
		

			<td valign='top'>
				This group includes the transport of passengers on inland waters, involving vessels that are not suitable for sea transport. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399123
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				50.30 
			</td>
		

			<td valign='top'>
				50.3 
			</td>
		

			<td valign='top'>
				Inland passenger water transport 
			</td>
		

			<td valign='top'>
				5021 
			</td>
		

			<td valign='top'>
				This class includes:- transport of passengers via rivers, canals, lakes and other inland waterways, including inside harbours and ports 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of pleasure boats with crew for inland water transport 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of pleasure boats and yachts without crew, see 77.21 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399124
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				50.4 
			</td>
		

			<td valign='top'>
				50 
			</td>
		

			<td valign='top'>
				Inland freight water transport 
			</td>
		

			<td valign='top'>
				502 
			</td>
		

			<td valign='top'>
				This group includes the transport of freight on inland waters, involving vessels that are not suitable for sea transport. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399125
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				50.40 
			</td>
		

			<td valign='top'>
				50.4 
			</td>
		

			<td valign='top'>
				Inland freight water transport 
			</td>
		

			<td valign='top'>
				5022 
			</td>
		

			<td valign='top'>
				This class includes:- transport of freight via rivers, canals, lakes and other inland waterways, including inside harbours and ports 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of vessels with crew for inland freight water transport 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- cargo handling, see 52.24- rental of commercial ships or boats without crew, see 77.34 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399126
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				51 
			</td>
		

			<td valign='top'>
				H 
			</td>
		

			<td valign='top'>
				Air transport 
			</td>
		

			<td valign='top'>
				51 
			</td>
		

			<td valign='top'>
				This division includes the transport of passengers or freight by air or via space. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- crop spraying, see 01.61- repair and maintenance of aircraft or aircraft engines, see 33.16- operation of airports, see 52.23- aerial advertising (sky-writing), see 73.11- aerial photography, see 74.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399127
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				51.1 
			</td>
		

			<td valign='top'>
				51 
			</td>
		

			<td valign='top'>
				Passenger air transport 
			</td>
		

			<td valign='top'>
				511 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399128
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				51.10 
			</td>
		

			<td valign='top'>
				51.1 
			</td>
		

			<td valign='top'>
				Passenger air transport 
			</td>
		

			<td valign='top'>
				5110 
			</td>
		

			<td valign='top'>
				This class includes:- transport of passengers by air over regular routes and on regular schedules- charter flights for passengers- scenic and sightseeing flights 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of air transport equipment with operator for the purpose of passenger transportation- general aviation activities, such as:  • transport of passengers by aero clubs for instruction or pleasure 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of air transport equipment without operator, see 77.35 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399129
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				51.2 
			</td>
		

			<td valign='top'>
				51 
			</td>
		

			<td valign='top'>
				Freight air transport and space transport 
			</td>
		

			<td valign='top'>
				512 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399130
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				51.21 
			</td>
		

			<td valign='top'>
				51.2 
			</td>
		

			<td valign='top'>
				Freight air transport 
			</td>
		

			<td valign='top'>
				5120 
			</td>
		

			<td valign='top'>
				This class includes:- transport freight by air over regular routes and on regular schedules- non-scheduled transport of freight by air 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of air transport equipment with operator for the purpose of freight transportation 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399131
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				51.22 
			</td>
		

			<td valign='top'>
				51.2 
			</td>
		

			<td valign='top'>
				Space transport 
			</td>
		

			<td valign='top'>
				5120 
			</td>
		

			<td valign='top'>
				This class includes:- launching of satellites and space vehicles- space transport of freight and passengers 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399132
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				52 
			</td>
		

			<td valign='top'>
				H 
			</td>
		

			<td valign='top'>
				Warehousing and support activities for transportation 
			</td>
		

			<td valign='top'>
				52 
			</td>
		

			<td valign='top'>
				This division includes warehousing and support activities for transportation, such as operating of transport infrastructure (e.g. airports, harbours, tunnels, bridges, etc.), the activities of transport agencies and cargo handling. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399133
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				52.1 
			</td>
		

			<td valign='top'>
				52 
			</td>
		

			<td valign='top'>
				Warehousing and storage 
			</td>
		

			<td valign='top'>
				521 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399134
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				52.10 
			</td>
		

			<td valign='top'>
				52.1 
			</td>
		

			<td valign='top'>
				Warehousing and storage 
			</td>
		

			<td valign='top'>
				5210 
			</td>
		

			<td valign='top'>
				This class includes:- operation of storage and warehouse facilities for all kinds of goods:  • operation of grain silos, general merchandise warehouses, refrigerated warehouses, storage tanks etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- storage of goods in foreign trade zones- blast freezing 
			</td>
		

			<td valign='top'>
				- Digitalisation and storage of files and documents 
			</td>
		

			<td valign='top'>
				This class excludes:- parking facilities for motor vehicles, see 52.21- operation of self storage facilities, see 68.20- rental of vacant space, see 68.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399135
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				52.2 
			</td>
		

			<td valign='top'>
				52 
			</td>
		

			<td valign='top'>
				Support activities for transportation 
			</td>
		

			<td valign='top'>
				522 
			</td>
		

			<td valign='top'>
				This group includes activities supporting the transport of passengers or freight, such as operation of parts of the transport infrastructure or activities related to handling freight immediately before or after transport or between transport segments. The operation and maintenance of all transport facilities is included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399136
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				52.21 
			</td>
		

			<td valign='top'>
				52.2 
			</td>
		

			<td valign='top'>
				Service activities incidental to land transportation 
			</td>
		

			<td valign='top'>
				5221 
			</td>
		

			<td valign='top'>
				This class includes:- activities related to land transport of passengers, animals or freight:  • operation of terminal facilities such as railway stations, bus stations, stations for the handling of goods  • operation of railroad infrastructure  • operation of roads, bridges, tunnels, car parks or garages, bicycle parkings, winter storage of caravans- switching and shunting- towing and road side assistance 
			</td>
		

			<td valign='top'>
				This class also includes:- liquefaction of gas for transportation purposes 
			</td>
		

			<td valign='top'>
				- Liquefaction of gas and regasification for transport purposes- Ensuring safety at railroad infrastructure maintenance work- Collection of fees for public transport- Natural gas liquefaction on own account 
			</td>
		

			<td valign='top'>
				This class excludes:- cargo handling, see 52.24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399137
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				52.22 
			</td>
		

			<td valign='top'>
				52.2 
			</td>
		

			<td valign='top'>
				Service activities incidental to water transportation 
			</td>
		

			<td valign='top'>
				5222 
			</td>
		

			<td valign='top'>
				This class includes:- activities related to water transport of passengers, animals or freight:  • operation of terminal facilities such as harbours and piers  • operation of waterway locks etc.  • navigation, pilotage and berthing activities  • lighterage, salvage activities  • lighthouse activities 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Ensuring safety at waterway infrastructure maintenance work 
			</td>
		

			<td valign='top'>
				This class excludes:- cargo handling, see 52.24- operation of marinas, see 93.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399138
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				52.23 
			</td>
		

			<td valign='top'>
				52.2 
			</td>
		

			<td valign='top'>
				Service activities incidental to air transportation 
			</td>
		

			<td valign='top'>
				5223 
			</td>
		

			<td valign='top'>
				This class includes:- activities related to air transport of passengers, animals or freight:  • operation of terminal facilities such as airway terminals etc.  • airport and air-traffic-control activities  • ground service activities on airfields etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- fire fighting and fire prevention services at airports 
			</td>
		

			<td valign='top'>
				- Ensuring safety at airfield infrastructure maintenance work- Air traffic control activities, at airports and for long distances 
			</td>
		

			<td valign='top'>
				This class excludes:- cargo handling, see 52.24- operation of flying schools, see 85.32, 85.53 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399139
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				52.24 
			</td>
		

			<td valign='top'>
				52.2 
			</td>
		

			<td valign='top'>
				Cargo handling 
			</td>
		

			<td valign='top'>
				5224 
			</td>
		

			<td valign='top'>
				This class includes:- loading and unloading of goods or passengers' luggage irrespective of the mode of transport used for transportation- stevedoring- loading and unloading of freight railway cars 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Rental and leasing of transtainers or luffing cranes, with operator, for cargo handling activities 
			</td>
		

			<td valign='top'>
				This class excludes:- operation of terminal facilities, see 52.21, 52.22 and 52.23 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399140
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				52.29 
			</td>
		

			<td valign='top'>
				52.2 
			</td>
		

			<td valign='top'>
				Other transportation support activities 
			</td>
		

			<td valign='top'>
				5229 
			</td>
		

			<td valign='top'>
				This class includes:- forwarding of freight- arranging or organising of transport operations by rail, road, sea or air- organisation of group and individual consignments (including pickup and delivery of goods and grouping of consignments)- issue and procurement of transport documents and waybills- activities of customs agents- activities of sea-freight forwarders and air-cargo agents- brokerage for ship and aircraft space- goods-handling operations, e.g. temporary crating for the sole purpose of protecting the goods during transit, uncrating, sampling, weighing of goods 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- courier activities, see 53.20- provision of motor, marine, aviation and transport insurance, see 65.12- activities of travel agencies, see 79.11- activities of tour operators, see 79.12- tourist assistance activities, see 79.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399141
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				53 
			</td>
		

			<td valign='top'>
				H 
			</td>
		

			<td valign='top'>
				Postal and courier activities 
			</td>
		

			<td valign='top'>
				53 
			</td>
		

			<td valign='top'>
				This division includes postal and courier activities, such as pickup, transport and delivery of letters and parcels under various arrangements. 
			</td>
		

			<td valign='top'>
				Local delivery and messenger services are also included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399142
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				53.1 
			</td>
		

			<td valign='top'>
				53 
			</td>
		

			<td valign='top'>
				Postal activities under universal service obligation 
			</td>
		

			<td valign='top'>
				531 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399143
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				53.10 
			</td>
		

			<td valign='top'>
				53.1 
			</td>
		

			<td valign='top'>
				Postal activities under universal service obligation 
			</td>
		

			<td valign='top'>
				5310 
			</td>
		

			<td valign='top'>
				This class includes the activities of postal services operating under a universal service obligation by one or more designated universal service providers. The activities include use of the universal service infrastructure, including retail locations, sorting and processing facilities, and carrier routes to pickup and deliver the mail. The delivery can include letter-post, i.e. letters, postcards, printed papers (newspaper, periodicals, advertising items, etc.), small packets, goods or documents. Also included are other services necessary to support the universal service obligation.This class includes:- pickup, sorting, transport and delivery (domestic or international) of letter-post and (mail-type) parcels and packages by postal services operating under a universal service obligation. One or more modes of transport may be involved and the activity may be carried out with either self-owned (private) transport or via public transport.- collection of letter-mail and parcels from public letter-boxes or from post offices 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- postal giro, postal savings activities and money order activities, see 64.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399144
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				53.2 
			</td>
		

			<td valign='top'>
				53 
			</td>
		

			<td valign='top'>
				Other postal and courier activities 
			</td>
		

			<td valign='top'>
				532 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399145
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				53.20 
			</td>
		

			<td valign='top'>
				53.2 
			</td>
		

			<td valign='top'>
				Other postal and courier activities 
			</td>
		

			<td valign='top'>
				5320 
			</td>
		

			<td valign='top'>
				This class includes:- pickup, sorting, transport and delivery (domestic or international) of letter-post and (mail-type) parcels and packages by firms operating outside the scope of a universal service obligation. One or more modes of transport may be involved and the activity may be carried out with either self-owned (private) transport or via public transport. 
			</td>
		

			<td valign='top'>
				This class also includes:- home delivery services 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- transport of freight, see (according to mode of transport) 49.20, 49.41, 50.20, 50.40, 51.21, 51.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399146
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				I 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				ACCOMMODATION AND FOOD SERVICE ACTIVITIES 
			</td>
		

			<td valign='top'>
				I 
			</td>
		

			<td valign='top'>
				This section includes the provision of short-stay accommodation for visitors and other travellers and the provision of complete meals and drinks fit for immediate consumption. The amount and type of supplementary services provided within this section can vary widely. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This section excludes the provision of long-term accommodation as primary residences, which is classified in real estate activities (section L). Also excluded is the preparation of food or drinks that are either not fit for immediate consumption or that are sold through independent distribution channels, i.e. through wholesale or retail trade activities. The preparation of these foods is classified in manufacturing (section C). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399147
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				55 
			</td>
		

			<td valign='top'>
				I 
			</td>
		

			<td valign='top'>
				Accommodation 
			</td>
		

			<td valign='top'>
				55 
			</td>
		

			<td valign='top'>
				This division includes the provision of short-stay accommodation for visitors and other travellers. 
			</td>
		

			<td valign='top'>
				Also included is the provision of longer term accommodation for students, workers and similar individuals. Some units may provide only accommodation while others provide a combination of accommodation, meals and/or recreational facilities. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes activities related to the provision of long-term primary residences in facilities such as apartments typically leased on a monthly or annual basis classified in real estate (section L). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399148
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				55.1 
			</td>
		

			<td valign='top'>
				55 
			</td>
		

			<td valign='top'>
				Hotels and similar accommodation 
			</td>
		

			<td valign='top'>
				551 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399149
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				55.10 
			</td>
		

			<td valign='top'>
				55.1 
			</td>
		

			<td valign='top'>
				Hotels and similar accommodation 
			</td>
		

			<td valign='top'>
				5510 
			</td>
		

			<td valign='top'>
				This class includes the provision of accommodation, typically on a daily or weekly basis, principally for short stays by visitors. This includes the provision of furnished accommodation in guest rooms and suites. Services include daily cleaning and bed-making. A range of additional services may be provided such as food and beverage services, parking, laundry services, swimming pools and exercise rooms, recreational facilities as well as conference and convention facilities.This class includes accommodation provided by:- hotels- resort hotels - suite/apartment hotels- motels 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Bed and breakfast units, with daily room cleaning and bed making 
			</td>
		

			<td valign='top'>
				This class excludes:- provision of homes and furnished or unfurnished flats or apartments for more permanent use, typically on a monthly or annual basis, see division 68 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399150
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				55.2 
			</td>
		

			<td valign='top'>
				55 
			</td>
		

			<td valign='top'>
				Holiday and other short-stay accommodation 
			</td>
		

			<td valign='top'>
				551 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399151
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				55.20 
			</td>
		

			<td valign='top'>
				55.2 
			</td>
		

			<td valign='top'>
				Holiday and other short-stay accommodation 
			</td>
		

			<td valign='top'>
				5510 
			</td>
		

			<td valign='top'>
				This class includes the provision of accommodation, typically on a daily or weekly basis, principally for short stays by visitors, in self-contained space consisting of complete furnished rooms or areas for living/dining and sleeping, with cooking facilities or fully equipped kitchens. This may take the form of apartments or flats in small free-standing multi-storey buildings or clusters of buildings, or single storey bungalows, chalets, cottages and cabins. Very minimal complementary services, if any, are provided. This class includes accommodation provided by:- children's and other holiday homes- visitor flats and bungalows- cottages and cabins without housekeeping services- youth hostels and mountain refuges 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Hotels with self-service, without daily room cleaning and bed making- Bed and breakfast units, without daily room cleaning and bed making 
			</td>
		

			<td valign='top'>
				This class excludes:- provision of furnished short-stay accommodation with daily cleaning, bed-making, food and beverage services, see 55.10- provision of homes and furnished or unfurnished flats or apartments for more permanent use, typically on a monthly or annual basis, see division 68 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399152
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				55.3 
			</td>
		

			<td valign='top'>
				55 
			</td>
		

			<td valign='top'>
				Camping grounds, recreational vehicle parks and trailer parks 
			</td>
		

			<td valign='top'>
				552 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399153
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				55.30 
			</td>
		

			<td valign='top'>
				55.3 
			</td>
		

			<td valign='top'>
				Camping grounds, recreational vehicle parks and trailer parks 
			</td>
		

			<td valign='top'>
				5520 
			</td>
		

			<td valign='top'>
				This class includes:- provision of accommodation in campgrounds, trailer parks, recreational camps and fishing and hunting camps for short stay visitors- provision of space and facilities for recreational vehicles 
			</td>
		

			<td valign='top'>
				This class also includes accommodation provided by:- protective shelters or plain bivouac facilities for placing tents and/or sleeping bags 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- mountain refuge, cabins and hostels, see 55.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399154
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				55.9 
			</td>
		

			<td valign='top'>
				55 
			</td>
		

			<td valign='top'>
				Other accommodation 
			</td>
		

			<td valign='top'>
				559 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399155
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				55.90 
			</td>
		

			<td valign='top'>
				55.9 
			</td>
		

			<td valign='top'>
				Other accommodation 
			</td>
		

			<td valign='top'>
				5590 
			</td>
		

			<td valign='top'>
				This class includes the provision temporary or longer-term accommodation in single or shared rooms or dormitories for students, migrant (seasonal) workers and other individuals. This class includes:- student residences- school dormitories- workers hostels - rooming and boarding houses- railway sleeping cars 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399156
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				56 
			</td>
		

			<td valign='top'>
				I 
			</td>
		

			<td valign='top'>
				Food and beverage service activities 
			</td>
		

			<td valign='top'>
				56 
			</td>
		

			<td valign='top'>
				This division includes food and beverage serving activities providing complete meals or drinks fit for immediate consumption, whether in traditional restaurants, self-service or take-away restaurants, whether as permanent or temporary stands with or without seating. Decisive is the fact that meals fit for immediate consumption are offered, not the kind of facility providing them. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				Excluded is the production of meals not fit for immediate consumption or not planned to be consumed immediately or of prepared food which is not considered to be a meal (see divisions 10: manufacture of food products and 11: manufacture of beverages). Also excluded is the sale of not self-manufactured food that is not considered to be a meal or of meals that are not fit for immediate consumption (see section G: wholesale and retail trade). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399157
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				56.1 
			</td>
		

			<td valign='top'>
				56 
			</td>
		

			<td valign='top'>
				Restaurants and mobile food service activities 
			</td>
		

			<td valign='top'>
				561 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399158
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				56.10 
			</td>
		

			<td valign='top'>
				56.1 
			</td>
		

			<td valign='top'>
				Restaurants and mobile food service activities 
			</td>
		

			<td valign='top'>
				5610 
			</td>
		

			<td valign='top'>
				This class includes the provision of food services to customers, whether they are served while seated or serve themselves from a display of items, whether they eat the prepared meals on the premises, take them out or have them delivered. This includes the preparation and serving of meals for immediate consumption from motorised vehicles or non-motorised carts.This class includes activities of:- restaurants- cafeterias- fast-food restaurants- take-out eating places- ice cream truck vendors- mobile food carts- food preparation in market stalls 
			</td>
		

			<td valign='top'>
				This class also includes:- restaurant and bar activities connected to transportation, when carried out by separate units 
			</td>
		

			<td valign='top'>
				- Delivery of self-prepared meals, such as pizza and sushi, for immediate consumption 
			</td>
		

			<td valign='top'>
				This class excludes:- retail sale of food through vending machines, see 47.99- concession operation of eating facilities, see 56.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399159
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				56.2 
			</td>
		

			<td valign='top'>
				56 
			</td>
		

			<td valign='top'>
				Event catering and other food service activities 
			</td>
		

			<td valign='top'>
				562 
			</td>
		

			<td valign='top'>
				This group includes catering activities for individual events or for a specified period of time and the operation of food concessions, such as at sports or similar facilities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399160
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				56.21 
			</td>
		

			<td valign='top'>
				56.2 
			</td>
		

			<td valign='top'>
				Event catering activities 
			</td>
		

			<td valign='top'>
				5621 
			</td>
		

			<td valign='top'>
				This class includes the provision of food services based on contractual arrangements with the customer, at the location specified by the customer, for a specific event. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of perishable food items for resale, see 10.89- retail sale of perishable food items, see division 47 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399161
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				56.29 
			</td>
		

			<td valign='top'>
				56.2 
			</td>
		

			<td valign='top'>
				Other food service activities 
			</td>
		

			<td valign='top'>
				5629 
			</td>
		

			<td valign='top'>
				This class includes industrial catering, i.e. the provision of food services based on contractual arrangements with the customer, for a specific period of time. Also included is the operation of food concessions at sports and similar facilities. The food is usually prepared in a central unit.This class includes:- activities of food service contractors (e.g. for transportation companies)- operation of food concessions at sports and similar facilities- operation of canteens or cafeterias (e.g. for factories, offices, hospitals or schools) on a concession basis 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- manufacture of perishable food items for resale, see 10.89- retail sale of perishable food items, see division 47 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399162
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				56.3 
			</td>
		

			<td valign='top'>
				56 
			</td>
		

			<td valign='top'>
				Beverage serving activities 
			</td>
		

			<td valign='top'>
				563 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399163
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				56.30 
			</td>
		

			<td valign='top'>
				56.3 
			</td>
		

			<td valign='top'>
				Beverage serving activities 
			</td>
		

			<td valign='top'>
				5630 
			</td>
		

			<td valign='top'>
				This class includes preparation and serving of beverages for immediate consumption on the premises.This class includes activities of:- bars- taverns- cocktail lounges- discotheques (with beverage serving predominant)- beer parlours- coffee shops- fruit juice bars- mobile beverage vendors 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- reselling packaged/prepared beverages, see 47- retail sale of beverages through vending machines, see 47.99- operation of discotheques and dance floors without beverage serving, see 93.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399164
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				J 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				INFORMATION AND COMMUNICATION 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				This section includes the production and distribution of information and cultural products, the provision of the means to transmit or distribute these products, as well as data or communications, information technology activities and the processing of data and other information service activities.The main components of this section are publishing activities (division 58), including software publishing, motion picture and sound recording activities (division 59), radio and TV broadcasting and programming activities (division 60), telecommunications activities (division 61), information technology activities (division 62) and other information service activities (division 63).Publishing includes the acquisition of copyrights for content (information products) and making this content available to the general public by engaging in (or arranging for) the reproduction and distribution of this content in various forms. All the feasible forms of publishing (in print, electronic or audio form, on the Internet, as multimedia products such as CD-ROM reference books etc.) are included in this section.Activities related to production and distribution of TV programming span divisions 59, 60 and 61, reflecting different stages in this process. Individual components, such as movies, television series etc. are produced by activities in division 59, while the creation of a complete television channel programme, from components produced in division 59 or other components (such as live news programming) is included in division 60. Division 60 also includes the broadcasting of this programme by the producer. The distribution of the complete television programme by third parties, i.e. without any alteration of the content, is included in division 61. This distribution in division 61 can be done through broadcasting, satellite or cable systems. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399165
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				58 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				Publishing activities 
			</td>
		

			<td valign='top'>
				58 
			</td>
		

			<td valign='top'>
				This division includes the publishing of books, brochures, leaflets, dictionaries, encyclopaedias, atlases, maps and charts; publishing of newspapers, journals and periodicals; directory and mailing list and other publishing, as well as software publishing.Publishing includes the acquisition of copyrights to content (information products) and making this content available to the general public by engaging in (or arranging for) the reproduction and distribution of this content in various forms. All the feasible forms of publishing (in print, electronic or audio form, on the Internet, as multimedia products such as CD-ROM reference books etc.), except publishing of motion pictures, are included in this division. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes the publishing of motion pictures, video tapes and movies on DVD or similar media (division 59) and the production of master copies for records or audio material (division 59). Also excluded is printing (see 18.11, 18.12) and the mass reproduction of recorded media (see 18.20). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399166
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				58.1 
			</td>
		

			<td valign='top'>
				58 
			</td>
		

			<td valign='top'>
				Publishing of books, periodicals and other publishing activities 
			</td>
		

			<td valign='top'>
				581 
			</td>
		

			<td valign='top'>
				This group includes activities of publishing books, newspapers, magazines and other periodicals, directories and mailing lists, and other works such as photos, engravings, postcards, timetables, forms, posters and reproductions of works of art. These works are characterised by the intellectual creativity required in their development and are usually protected by copyright. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399167
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.11 
			</td>
		

			<td valign='top'>
				58.1 
			</td>
		

			<td valign='top'>
				Book publishing 
			</td>
		

			<td valign='top'>
				5811 
			</td>
		

			<td valign='top'>
				This class includes the activities of publishing of books in print, electronic (CD, electronic displays etc.) or audio form or on the Internet.Included are:- publishing of books, brochures, leaflets and similar publications, including publishing of dictionaries and encyclopaedias- publishing of atlases, maps and charts- publishing of audio books- publishing of encyclopaedias etc. on CD-ROM 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of globes, see 32.99- publishing of advertising material, see 58.19- publishing of music and sheet books, see 59.20- activities of independent authors, see 90.03 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399168
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.12 
			</td>
		

			<td valign='top'>
				58.1 
			</td>
		

			<td valign='top'>
				Publishing of directories and mailing lists 
			</td>
		

			<td valign='top'>
				5812 
			</td>
		

			<td valign='top'>
				This class includes the publishing of lists of facts/information (databases), that are protected in their form, but not in their content. These lists can be published in printed or electronic form.This class includes:- publishing of mailing lists- publishing of telephone books- publishing of other directories and compilations, such as case law, pharmaceutical compendia etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399169
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.13 
			</td>
		

			<td valign='top'>
				58.1 
			</td>
		

			<td valign='top'>
				Publishing of newspapers 
			</td>
		

			<td valign='top'>
				5813 
			</td>
		

			<td valign='top'>
				This class includes the publishing of newspapers, including advertising newspapers, appearing at least four times a week. Publishing can be done in print or electronic form, including on the Internet. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- news agency activities, see 63.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399170
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.14 
			</td>
		

			<td valign='top'>
				58.1 
			</td>
		

			<td valign='top'>
				Publishing of journals and periodicals 
			</td>
		

			<td valign='top'>
				5813 
			</td>
		

			<td valign='top'>
				This class includes the publishing of periodicals and other journals, appearing less than four times a week. Publishing can be done in print or electronic form, including on the Internet. Publishing of radio and television schedules is included here. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399171
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.19 
			</td>
		

			<td valign='top'>
				58.1 
			</td>
		

			<td valign='top'>
				Other publishing activities 
			</td>
		

			<td valign='top'>
				5819 
			</td>
		

			<td valign='top'>
				This class includes:- publishing (including on-line) of:  • catalogues   • photos, engravings and postcards  • greeting cards  • forms  • posters, reproduction of works of art  • advertising material  • other printed matter- on-line publishing of statistics and other information 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Publishing of blogs 
			</td>
		

			<td valign='top'>
				This class excludes:- publishing of advertising newspapers, see 58.13- on-line provision of software (application hosting and application service provisioning), see 63.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399172
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				58.2 
			</td>
		

			<td valign='top'>
				58 
			</td>
		

			<td valign='top'>
				Software publishing 
			</td>
		

			<td valign='top'>
				582 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399173
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.21 
			</td>
		

			<td valign='top'>
				58.2 
			</td>
		

			<td valign='top'>
				Publishing of computer games 
			</td>
		

			<td valign='top'>
				5820 
			</td>
		

			<td valign='top'>
				This class includes: - publishing of computer games for all platforms 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Operation of On-line games 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399174
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				58.29 
			</td>
		

			<td valign='top'>
				58.2 
			</td>
		

			<td valign='top'>
				Other software publishing 
			</td>
		

			<td valign='top'>
				5820 
			</td>
		

			<td valign='top'>
				This class includes:- publishing of ready-made (non-customised) software, including translation or adaptation of non-customised software for a particular market on own account:  • operating systems  • business and other applications 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Development and publishing of software for microchip design 
			</td>
		

			<td valign='top'>
				This class excludes:- reproduction of software, see 18.20- retail sale of non-customised software, see 47.41- production of software not associated with publishing, including translation or adaptation of non-customised software for a particular market on a fee or contract basis, see 62.01- on-line provision of software (application hosting and application service provisioning), see 63.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399175
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				59 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				Motion picture, video and television programme production, sound recording and music publishing activities 
			</td>
		

			<td valign='top'>
				59 
			</td>
		

			<td valign='top'>
				This division includes production of theatrical and non-theatrical motion pictures whether on film, video tape or disc for direct projection in theatres or for broadcasting on television; supporting activities such as film editing, cutting, dubbing etc.; distribution of motion pictures and other film productions to other industries; as well as motion picture or other film productions projection. Buying and selling of motion picture or other film productions distribution rights is also included. 
			</td>
		

			<td valign='top'>
				This division also includes the sound recording activities, i.e. production of original sound master recordings, releasing, promoting and distributing them, publishing of music as well as sound recording service activities in a studio or elsewhere. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399176
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				59.1 
			</td>
		

			<td valign='top'>
				59 
			</td>
		

			<td valign='top'>
				Motion picture, video and television programme activities 
			</td>
		

			<td valign='top'>
				591 
			</td>
		

			<td valign='top'>
				This group includes production of theatrical and non-theatrical motion pictures whether on film, video tape, DVD or other media, including digital distribution, for direct projection in theatres or for broadcasting on television; supporting activities such as film editing, cutting, dubbing etc.; distribution of motion pictures or other film productions (video tapes, DVDs, etc) to other industries; as well as their projection. 
			</td>
		

			<td valign='top'>
				Buying and selling of motion picture or any other film production distribution rights is also included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399177
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				59.11 
			</td>
		

			<td valign='top'>
				59.1 
			</td>
		

			<td valign='top'>
				Motion picture, video and television programme production activities 
			</td>
		

			<td valign='top'>
				5911 
			</td>
		

			<td valign='top'>
				This class includes:- production of motion pictures, videos, television programmes (televisions series, documentaries etc.), or television advertisements 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- film duplicating (except reproduction of motion picture film for theatrical distribution) as well as audio and video tape, CD or DVD reproduction from master copies, see 18.20- wholesale of recorded video tapes, CDs, DVDs, see 46.43- wholesale of blank video tapes, CDs, see 46.52- retail trade of video tapes, CDs, DVDs, see 47.63- post-production activities, see 59.12- sound recording and recording of books on tape, see 59.20- television broadcasting, see 60.2- creating a complete television channel programme, see 60.2- film processing other than for the motion picture industry, see 74.20- activities of personal theatrical or artistic agents or agencies, see 74.90- rental of video tapes, DVDs to the general public, see 77.22- real-time (i.e. simultaneous) closed captioning of live television performances, meetings, conferences, etc., see 82.99- activities of own account actors, cartoonists, directors, stage designers and technical specialists, see 90.0 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399178
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				59.12 
			</td>
		

			<td valign='top'>
				59.1 
			</td>
		

			<td valign='top'>
				Motion picture, video and television programme post-production activities 
			</td>
		

			<td valign='top'>
				5912 
			</td>
		

			<td valign='top'>
				This class includes post-production activities such as editing, film/tape transfers, titling, subtitling, credits, closed captioning, computer-produced graphics, animation and special effects, developing and processing motion picture film, as well as activities of motion picture film laboratories and activities of special laboratories for animated films. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of stock footage film libraries etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- film duplicating (except reproduction of motion picture film for theatrical distribution) as well as audio and video tape, CD or DVD reproduction from master copies, see 18.20- wholesale of recorded video tapes, CDs, DVDs, see 46.43- wholesale of blank video tapes, CDs, see 46.52- retail trade of video tapes, CDs, DVDs, see 47.63- film processing other than for the motion picture industry, see 74.20- rental of video tapes, DVDs to the general public, see 77.22- activities of own account actors, cartoonists, directors, stage designers and technical specialists, see 90.0 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399179
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				59.13 
			</td>
		

			<td valign='top'>
				59.1 
			</td>
		

			<td valign='top'>
				Motion picture, video and television programme distribution activities 
			</td>
		

			<td valign='top'>
				5913 
			</td>
		

			<td valign='top'>
				This class includes:- distributing film, video tapes, DVDs and similar productions to motion picture theatres, television networks and stations, and exhibitors 
			</td>
		

			<td valign='top'>
				This class also includes:- acquiring film, video tape and DVD distribution rights 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- film duplicating as well as audio and video tape, CD or DVD reproduction from master copies, see 18.20- wholesale of recorded video tapes and DVDs, see 46.43- retail sale of recorded video tapes and DVDs, see 47.63 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399180
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				59.14 
			</td>
		

			<td valign='top'>
				59.1 
			</td>
		

			<td valign='top'>
				Motion picture projection activities 
			</td>
		

			<td valign='top'>
				5914 
			</td>
		

			<td valign='top'>
				This class includes:- activities of motion picture or video tape projection in cinemas, in the open air or in other projection facilities- activities of cine-clubs 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399181
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				59.2 
			</td>
		

			<td valign='top'>
				59 
			</td>
		

			<td valign='top'>
				Sound recording and music publishing activities 
			</td>
		

			<td valign='top'>
				592 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399182
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				59.20 
			</td>
		

			<td valign='top'>
				59.2 
			</td>
		

			<td valign='top'>
				Sound recording and music publishing activities 
			</td>
		

			<td valign='top'>
				5920 
			</td>
		

			<td valign='top'>
				This class includes the activities of production of original (sound) master recordings, such as tapes, CDs; releasing, promoting and distributing sound recordings to wholesalers, retailers or directly to the public. These activities might be integrated or not with the production of master recordings in the same unit. If not, the unit exercising these activities has to obtain the reproduction and distribution rights to master recordings. 
			</td>
		

			<td valign='top'>
				This class also includes sound recording service activities in a studio or elsewhere, including the production of taped ( i.e. non-live) radio programming.This class also includes the activities of music publishing, i.e. activities of acquiring and registering copyrights for musical compositions, promoting, authorising and using these compositions in recordings, radio, television, motion pictures, live performances, print and other media. Units engaged in these activities may own the copyright or act as administrator of the music copyrights on behalf of the copyright owners. Publishing of music and sheet books is included here. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399183
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				60 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				Programming and broadcasting activities 
			</td>
		

			<td valign='top'>
				60 
			</td>
		

			<td valign='top'>
				This division includes the activities of creating content or acquiring the right to distribute content and subsequently broadcasting that content, such as radio, television and data programs of entertainment, news, talk, and the like. Also included is data broadcasting, typically integrated with radio or TV broadcasting. The broadcasting can be performed using different technologies, over-the-air, via satellite, via a cable network or via Internet. 
			</td>
		

			<td valign='top'>
				This division also includes the production of programs that are typically narrowcast in nature (limited format, such as news, sports, education, and youth-oriented programming) on a subscription or fee basis, to a third party, for subsequent broadcasting to the public. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes the distribution of cable and other subscription programming (see division 61). 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399184
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				60.1 
			</td>
		

			<td valign='top'>
				60 
			</td>
		

			<td valign='top'>
				Radio broadcasting 
			</td>
		

			<td valign='top'>
				601 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399185
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				60.10 
			</td>
		

			<td valign='top'>
				60.1 
			</td>
		

			<td valign='top'>
				Radio broadcasting 
			</td>
		

			<td valign='top'>
				6010 
			</td>
		

			<td valign='top'>
				This class includes:- activities of broadcasting audio signals through radio broadcasting studios and facilities for the transmission of aural programming to the public, to affiliates or to subscribers 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of radio networks, i.e. assembling and transmitting aural programming to the affiliates or subscribers via over-the-air broadcasts, cable or satellite- radio broadcasting activities over the Internet (Internet radio stations) - data broadcasting integrated with radio broadcasting 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- the production of taped radio programming, see 59.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399186
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				60.2 
			</td>
		

			<td valign='top'>
				60 
			</td>
		

			<td valign='top'>
				Television programming and broadcasting activities 
			</td>
		

			<td valign='top'>
				602 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399187
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				60.20 
			</td>
		

			<td valign='top'>
				60.2 
			</td>
		

			<td valign='top'>
				Television programming and broadcasting activities 
			</td>
		

			<td valign='top'>
				6020 
			</td>
		

			<td valign='top'>
				This class includes the creation of a complete television channel programme, from purchased programme components (e.g. movies, documentaries etc.), self produced programme components (e.g. local news, live reports) or a combination thereof. This complete television programme can be either broadcast by the producing unit or produced for transmission by a third party distributor, such as cable companies or satellite television providers.The programming may be of a general or specialised nature (e.g. limited formats such as news, sports, education or youth oriented programming). This class includes programming that is made freely available to users, as well as programming that is available only on a subscription basis. The programming of video-on-demand channels is also included here. 
			</td>
		

			<td valign='top'>
				This class also includes data broadcasting integrated with television broadcasting. 
			</td>
		

			<td valign='top'>
				- Broadcasting of live theatrical performances via Internet 
			</td>
		

			<td valign='top'>
				This class excludes:- the production of television programme elements (movies, documentaries, talk shows, commercials etc.) not associated with broadcasting, see 59.11- the assembly of a package of channels and distribution of that package, without programming, see division 61 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399188
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				61 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				Telecommunications 
			</td>
		

			<td valign='top'>
				61 
			</td>
		

			<td valign='top'>
				This division includes the activities of providing telecommunications and related service activities, that is transmitting voice, data, text, sound and video. The transmission facilities that carry out these activities may be based on a single technology or a combination of technologies. The commonality of activities classified in this division is the transmission of content, without being involved in its creation. The breakdown in this division is based on the type of infrastructure operated.In the case of transmission of television signals this may include the bundling of complete programming channels (produced in division 60) in to programme packages for distribution. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399189
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				61.1 
			</td>
		

			<td valign='top'>
				61 
			</td>
		

			<td valign='top'>
				Wired telecommunications activities 
			</td>
		

			<td valign='top'>
				611 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399190
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				61.10 
			</td>
		

			<td valign='top'>
				61.1 
			</td>
		

			<td valign='top'>
				Wired telecommunications activities 
			</td>
		

			<td valign='top'>
				6110 
			</td>
		

			<td valign='top'>
				This class includes:- operating, maintaining or providing access to facilities for the transmission of voice, data, text, sound and video using a wired telecommunications infrastructure, including:  • operating and maintaining switching and transmission facilities to provide point-to-point communications via landlines, microwave or a combination of landlines and satellite linkups  • operating of cable distribution systems (e.g. for distribution of data and television signals)  • furnishing telegraph and other non-vocal communications using own facilitiesThe transmission facilities that carry out these activities, may be based on a single technology or a combination of technologies. 
			</td>
		

			<td valign='top'>
				This class also includes:- purchasing access and network capacity from owners and operators of networks and providing telecommunications services using this capacity to businesses and households- provision of Internet access by the operator of the wired infrastructure 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- telecommunications resellers, see 61.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399191
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				61.2 
			</td>
		

			<td valign='top'>
				61 
			</td>
		

			<td valign='top'>
				Wireless telecommunications activities 
			</td>
		

			<td valign='top'>
				612 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399192
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				61.20 
			</td>
		

			<td valign='top'>
				61.2 
			</td>
		

			<td valign='top'>
				Wireless telecommunications activities 
			</td>
		

			<td valign='top'>
				6120 
			</td>
		

			<td valign='top'>
				This class includes:- operating, maintaining or providing access to facilities for the transmission of voice, data, text, sound, and video using a wireless telecommunications infrastructure- maintaining and operating paging as well as cellular and other wireless telecommunications networksThe transmission facilities provide omni-directional transmission via airwaves and may be based on a single technology or a combination of technologies. 
			</td>
		

			<td valign='top'>
				This class also includes:- purchasing access and network capacity from owners and operators of networks and providing wireless telecommunications services (except satellite) using this capacity to businesses and households- provision of Internet access by the operator of the wireless infrastructure 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- telecommunications resellers, see 61.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399193
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				61.3 
			</td>
		

			<td valign='top'>
				61 
			</td>
		

			<td valign='top'>
				Satellite telecommunications activities 
			</td>
		

			<td valign='top'>
				613 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399194
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				61.30 
			</td>
		

			<td valign='top'>
				61.3 
			</td>
		

			<td valign='top'>
				Satellite telecommunications activities 
			</td>
		

			<td valign='top'>
				6130 
			</td>
		

			<td valign='top'>
				This class includes:- operating, maintaining or providing access to facilities for the transmission of voice, data, text, sound and video using a satellite telecommunications infrastructure- delivery of visual, aural or textual programming received from cable networks, local television stations or radio networks to consumers via direct-to-home satellite systems. (The units classified here do not generally originate programming material.) 
			</td>
		

			<td valign='top'>
				This class also includes:- provision of Internet access by the operator of the satellite infrastructure 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- telecommunications resellers, see 61.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399195
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				61.9 
			</td>
		

			<td valign='top'>
				61 
			</td>
		

			<td valign='top'>
				Other telecommunications activities 
			</td>
		

			<td valign='top'>
				619 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399196
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				61.90 
			</td>
		

			<td valign='top'>
				61.9 
			</td>
		

			<td valign='top'>
				Other telecommunications activities 
			</td>
		

			<td valign='top'>
				6190 
			</td>
		

			<td valign='top'>
				This class includes:- provision of specialised telecommunications applications, such as satellite tracking, communications telemetry, and radar station operations- operation of satellite terminal stations and associated facilities operationally connected with one or more terrestrial communications systems and capable of transmitting telecommunications to or receiving telecommunications from satellite systems- provision of Internet access over networks between the client and the ISP not owned or controlled by the ISP, such as dial-up Internet access etc.- provision of telephone and Internet access in facilities open to the public- provision of telecommunications services over existing telecom connections:  • VOIP (Voice Over Internet Protocol) provision- telecommunications resellers (i.e. purchasing and reselling network capacity without providing additional services) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Transmission via Internet of text messaging (SMS)- Wholesale trade of internet access packages and phone cards- Internet cafes- Sale of prepaid telephone cards for mobile phones 
			</td>
		

			<td valign='top'>
				This class excludes:- provision of Internet access by operators of telecommunications infrastructure, see 61.10, 61.20, 61.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399197
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				62 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				Computer programming, consultancy and related activities 
			</td>
		

			<td valign='top'>
				62 
			</td>
		

			<td valign='top'>
				This division includes the following activities of providing expertise in the field of information technologies: writing, modifying, testing and supporting software; planning and designing computer systems that integrate computer hardware, software and communication technologies; on-site management and operation of clients' computer systems and/or data processing facilities; and other professional and technical computer-related activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399198
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				62.0 
			</td>
		

			<td valign='top'>
				62 
			</td>
		

			<td valign='top'>
				Computer programming, consultancy and related activities 
			</td>
		

			<td valign='top'>
				620 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399199
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				62.01 
			</td>
		

			<td valign='top'>
				62.0 
			</td>
		

			<td valign='top'>
				Computer programming activities 
			</td>
		

			<td valign='top'>
				6201 
			</td>
		

			<td valign='top'>
				This class includes the writing, modifying, testing and supporting of software.This class includes:- designing the structure and content of, and/or writing the computer code necessary to create and implement:  • systems software (including updates and patches)  • software applications (including updates and patches)  • databases  • web pages- customising of software, i.e. modifying and configuring an existing application so that it is functional within the clients' information system environment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- publishing packaged software, see 58.29- translation or adaptation of non-customised software for a particular market on own account, see 58.29- planning and designing computer systems that integrate computer hardware, software and communication technologies, even though providing software might be an integral part, see 62.02 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399200
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				62.02 
			</td>
		

			<td valign='top'>
				62.0 
			</td>
		

			<td valign='top'>
				Computer consultancy activities 
			</td>
		

			<td valign='top'>
				6202 
			</td>
		

			<td valign='top'>
				This class includes the planning and designing of computer systems which integrate computer hardware, software and communication technologies. Services may include related users training. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- sale of computer hardware or software, see 46.51, 47.41- installation of mainframe and similar computers, see 33.20- installation (setting-up) of personal computers, see 62.09- installation of software, computer disaster recovery, see 62.09 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399201
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				62.03 
			</td>
		

			<td valign='top'>
				62.0 
			</td>
		

			<td valign='top'>
				Computer facilities management activities 
			</td>
		

			<td valign='top'>
				6202 
			</td>
		

			<td valign='top'>
				This class includes the provision of on-site management and operation of clients' computer systems and/or data processing facilities, as well as related support services. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399202
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				62.09 
			</td>
		

			<td valign='top'>
				62.0 
			</td>
		

			<td valign='top'>
				Other information technology and computer service activities 
			</td>
		

			<td valign='top'>
				6209 
			</td>
		

			<td valign='top'>
				This class includes other information technology and computer related activities not elsewhere classified, such as:- computer disaster recovery services- installation (setting-up) of personal computers- software installation services 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of mainframe and similar computers, see 33.20- computer programming, see 62.01- computer consultancy, see 62.02- computer facilities management, see 62.03- data processing and hosting, see 63.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399203
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				63 
			</td>
		

			<td valign='top'>
				J 
			</td>
		

			<td valign='top'>
				Information service activities 
			</td>
		

			<td valign='top'>
				63 
			</td>
		

			<td valign='top'>
				This division includes the activities of web search portals, data processing and hosting activities, as well as other activities that primarily supply information. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399204
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				63.1 
			</td>
		

			<td valign='top'>
				63 
			</td>
		

			<td valign='top'>
				Data processing, hosting and related activities; web portals 
			</td>
		

			<td valign='top'>
				631 
			</td>
		

			<td valign='top'>
				This group includes the provision of infrastructure for hosting, data processing services and related activities, as well as the provision of search facilities and other portals for the Internet. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399205
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				63.11 
			</td>
		

			<td valign='top'>
				63.1 
			</td>
		

			<td valign='top'>
				Data processing, hosting and related activities 
			</td>
		

			<td valign='top'>
				6311 
			</td>
		

			<td valign='top'>
				This class includes:- provision of infrastructure for hosting, data processing services and related activities- specialized hosting activities such as:  • Web hosting  • streaming services  • application hosting- application service provisioning- general time-share provision of mainframe facilities to clients- data processing activities:  • complete processing of data supplied by clients  • generation of specialized reports from data supplied by clients- provision of data entry services 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities where the supplier uses the computers only as a tool are classified according to the nature of the services rendered 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399206
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				63.12 
			</td>
		

			<td valign='top'>
				63.1 
			</td>
		

			<td valign='top'>
				Web portals 
			</td>
		

			<td valign='top'>
				6312 
			</td>
		

			<td valign='top'>
				This class includes: - operation of web sites that use a search engine to generate and maintain extensive databases of Internet addresses and content in an easily searchable format- operation of other websites that act as portals to the Internet, such as media sites providing periodically updated content 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- publishing of books, newspapers, journals etc. via Internet, see division 58- broadcasting via Internet, see division 60 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399207
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				63.9 
			</td>
		

			<td valign='top'>
				63 
			</td>
		

			<td valign='top'>
				Other information service activities 
			</td>
		

			<td valign='top'>
				639 
			</td>
		

			<td valign='top'>
				This group includes the activities of news agencies and all other remaining information service activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- activities of libraries and archives, see 91.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399208
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				63.91 
			</td>
		

			<td valign='top'>
				63.9 
			</td>
		

			<td valign='top'>
				News agency activities 
			</td>
		

			<td valign='top'>
				6391 
			</td>
		

			<td valign='top'>
				This class includes:- news syndicate and news agency activities furnishing news, pictures and features to the media 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of independent photojournalists, see 74.20- activities of independent journalists, see 90.03 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399209
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				63.99 
			</td>
		

			<td valign='top'>
				63.9 
			</td>
		

			<td valign='top'>
				Other information service activities n.e.c. 
			</td>
		

			<td valign='top'>
				6399 
			</td>
		

			<td valign='top'>
				This class includes other information service activities not elsewhere classified such as:- computer-based telephone information services- information search services on a contract or fee basis- news clipping services, press clipping services, etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Sports live data collection activities 
			</td>
		

			<td valign='top'>
				This class excludes:- activities of call centres, see 82.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399210
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				K 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				FINANCIAL AND INSURANCE ACTIVITIES 
			</td>
		

			<td valign='top'>
				K 
			</td>
		

			<td valign='top'>
				This section includes financial service activities, including insurance, reinsurance and pension funding activities and activities to support financial services. 
			</td>
		

			<td valign='top'>
				This section also includes the activities of holding assets, such as activities of holding companies and the activities of trusts, funds and similar financial entities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399211
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				64 
			</td>
		

			<td valign='top'>
				K 
			</td>
		

			<td valign='top'>
				Financial service activities, except insurance and pension funding 
			</td>
		

			<td valign='top'>
				64 
			</td>
		

			<td valign='top'>
				This division includes the activities of obtaining and redistributing funds other than for the purpose of insurance or pension funding or compulsory social security.Note: National institutional arrangements are likely to play a significant role in determining the classification within this division. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399212
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				64.1 
			</td>
		

			<td valign='top'>
				64 
			</td>
		

			<td valign='top'>
				Monetary intermediation 
			</td>
		

			<td valign='top'>
				641 
			</td>
		

			<td valign='top'>
				This group includes the obtaining of funds in the form of transferable deposits, i.e. funds that are fixed in money terms, obtained on a day-to-day basis and, apart from central banking, obtained from non-financial sources. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399213
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.11 
			</td>
		

			<td valign='top'>
				64.1 
			</td>
		

			<td valign='top'>
				Central banking 
			</td>
		

			<td valign='top'>
				6411 
			</td>
		

			<td valign='top'>
				This class includes:- issuing and managing the country's currency- monitoring and control of the money supply- taking deposits that are used for clearance between financial institutions- supervising banking operations- holding the country's international reserves- acting as banker to the governmentThe activities of central banks will vary for institutional reasons. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399214
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.19 
			</td>
		

			<td valign='top'>
				64.1 
			</td>
		

			<td valign='top'>
				Other monetary intermediation 
			</td>
		

			<td valign='top'>
				6419 
			</td>
		

			<td valign='top'>
				This class includes the receiving of deposits and/or close substitutes for deposits and extending of credit or lending funds. The granting of credit can take a variety of forms, such as loans, mortgages, credit cards etc. These activities are generally carried out by monetary institutions other than central banks, such as:- banks- savings banks- credit unions 
			</td>
		

			<td valign='top'>
				This class also includes:- postal giro and postal savings bank activities- credit granting for house purchase by specialised deposit-taking institutions- money order activities 
			</td>
		

			<td valign='top'>
				- Electronic currency issuing and trading 
			</td>
		

			<td valign='top'>
				This class excludes:- credit granting for house purchase by specialised non-depository institutions, see 64.92- credit card transaction processing and settlement activities, see 66.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399215
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				64.2 
			</td>
		

			<td valign='top'>
				64 
			</td>
		

			<td valign='top'>
				Activities of holding companies 
			</td>
		

			<td valign='top'>
				642 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399216
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.20 
			</td>
		

			<td valign='top'>
				64.2 
			</td>
		

			<td valign='top'>
				Activities of holding companies 
			</td>
		

			<td valign='top'>
				6420 
			</td>
		

			<td valign='top'>
				This class includes the activities of holding companies, i.e. units that hold the assets (owning controlling-levels of equity) of a group of subsidiary corporations and whose principal activity is owning the group. The holding companies in this class do not provide any other service to the businesses in which the equity is held, i.e. they do not administer or manage other units. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- active management of companies and enterprises, strategic planning and decision making of the company, see 70.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399217
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				64.3 
			</td>
		

			<td valign='top'>
				64 
			</td>
		

			<td valign='top'>
				Trusts, funds and similar financial entities 
			</td>
		

			<td valign='top'>
				643 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399218
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.30 
			</td>
		

			<td valign='top'>
				64.3 
			</td>
		

			<td valign='top'>
				Trusts, funds and similar financial entities 
			</td>
		

			<td valign='top'>
				6430 
			</td>
		

			<td valign='top'>
				This class includes legal entities organised to pool securities or other financial assets, without managing, on behalf of shareholders or beneficiaries. The portfolios are customised to achieve specific investment characteristics, such as diversification, risk, rate of return and price volatility. These entities earn interest, dividends and other property income, but have little or no employment and no revenue from the sale of services.This class includes:- open-end investment funds- closed-end investment funds- trusts, estates or agency accounts, administered on behalf of the beneficiaries under the terms of a trust agreement, will or agency agreement- unit investment trust funds 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- funds and trusts that earn revenue from the sale of goods or services, see NACE class according to their principal activity- activities of holding companies, see 64.20- pension funding, see 65.30- management of funds, see 66.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399219
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				64.9 
			</td>
		

			<td valign='top'>
				64 
			</td>
		

			<td valign='top'>
				Other financial service activities, except insurance and pension funding 
			</td>
		

			<td valign='top'>
				649 
			</td>
		

			<td valign='top'>
				This group includes financial service activities other than those conducted by monetary institutions. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- insurance and pension funding, see division 65 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399220
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.91 
			</td>
		

			<td valign='top'>
				64.9 
			</td>
		

			<td valign='top'>
				Financial leasing 
			</td>
		

			<td valign='top'>
				6491 
			</td>
		

			<td valign='top'>
				This class includes:- leasing where the term approximately covers the expected life of the asset and the lessee acquires substantially all the benefits of its use and takes all the risks associated with its ownership. The ownership of the asset may or may not eventually be transferred. Such leases cover all or virtually all costs including interest. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operational leasing, see division 77, according to type of goods leased 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399221
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.92 
			</td>
		

			<td valign='top'>
				64.9 
			</td>
		

			<td valign='top'>
				Other credit granting 
			</td>
		

			<td valign='top'>
				6492 
			</td>
		

			<td valign='top'>
				This class includes:- financial service activities primarily concerned with making loans by institutions not involved in monetary intermediation, where the granting of credit can take a variety of forms, such as loans, mortgages, credit cards etc., providing the following types of services:  • granting of consumer credit  • international trade financing  • provision of long-term finance to industry by industrial banks  • money lending outside the banking system  • credit granting for house purchase by specialised non-depository institutions  • pawnshops and pawnbrokers 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- credit granting for house purchase by specialised institutions that also take deposits, see 64.19- operational leasing, see division 77, according to type of goods leased- grant giving activities by membership organisations, see 94.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399222
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				64.99 
			</td>
		

			<td valign='top'>
				64.9 
			</td>
		

			<td valign='top'>
				Other financial service activities, except insurance and pension funding n.e.c. 
			</td>
		

			<td valign='top'>
				6499 
			</td>
		

			<td valign='top'>
				This class includes:- other financial service activities primarily concerned with distributing funds other than by making loans:  • factoring activities  • writing of swaps, options and other hedging arrangements  • activities of viatical settlement companies- own-account investment activities, such as by venture capital companies, investment clubs etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Acting as a buyer for the seller of swaps and options, and as seller for the buyer of swaps and options, i.e. acting as both buyer and seller, i.e. on own account- Purchase and brokerage of receivables 
			</td>
		

			<td valign='top'>
				This class excludes:- financial leasing, see 64.91- security dealing on behalf of others, see 66.12- trade, leasing and rental of real estate property, see division 68- bill collection without debt buying up, see 82.91- grant-giving activities by membership organisations, see 94.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399223
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				65 
			</td>
		

			<td valign='top'>
				K 
			</td>
		

			<td valign='top'>
				Insurance, reinsurance and pension funding, except compulsory social security 
			</td>
		

			<td valign='top'>
				65 
			</td>
		

			<td valign='top'>
				This division includes the underwriting annuities and insurance policies and investing premiums to build up a portfolio of financial assets to be used against future claims. Provision of direct insurance and reinsurance are included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399224
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				65.1 
			</td>
		

			<td valign='top'>
				65 
			</td>
		

			<td valign='top'>
				Insurance 
			</td>
		

			<td valign='top'>
				651 
			</td>
		

			<td valign='top'>
				This group includes life insurance with or without a substantial savings element and non-life insurance. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399225
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				65.11 
			</td>
		

			<td valign='top'>
				65.1 
			</td>
		

			<td valign='top'>
				Life insurance 
			</td>
		

			<td valign='top'>
				6511 
			</td>
		

			<td valign='top'>
				This class includes:- underwriting annuities and life insurance policies, disability income insurance policies, and accidental death and dismemberment insurance policies (with or without a substantial savings element) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Death or funeral insurance 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399226
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				65.12 
			</td>
		

			<td valign='top'>
				65.1 
			</td>
		

			<td valign='top'>
				Non-life insurance 
			</td>
		

			<td valign='top'>
				6512 
			</td>
		

			<td valign='top'>
				This class includes:- provision of insurance services other than life insurance:  • accident and fire insurance  • health insurance  • travel insurance  • property insurance  • motor, marine, aviation and transport insurance  • pecuniary loss and liability insurance 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Provision of motor vehicle warranty- Health plan management on own account 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399227
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				65.2 
			</td>
		

			<td valign='top'>
				65 
			</td>
		

			<td valign='top'>
				Reinsurance 
			</td>
		

			<td valign='top'>
				652 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399228
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				65.20 
			</td>
		

			<td valign='top'>
				65.2 
			</td>
		

			<td valign='top'>
				Reinsurance 
			</td>
		

			<td valign='top'>
				6520 
			</td>
		

			<td valign='top'>
				This class includes:- activities of assuming all or part of the risk associated with existing insurance policies originally underwritten by other insurance carriers 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399229
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				65.3 
			</td>
		

			<td valign='top'>
				65 
			</td>
		

			<td valign='top'>
				Pension funding 
			</td>
		

			<td valign='top'>
				653 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399230
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				65.30 
			</td>
		

			<td valign='top'>
				65.3 
			</td>
		

			<td valign='top'>
				Pension funding 
			</td>
		

			<td valign='top'>
				6530 
			</td>
		

			<td valign='top'>
				This class includes legal entities (i.e. funds, plans and/or programmes) organised to provide retirement income benefits exclusively for the sponsor's employees or members. This includes pension plans with defined benefits, as well as individual plans where benefits are simply defined through the member’s contribution.This class includes:- employee benefit plans- pension funds and plans- retirement plans 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Fund management on behalf of the members 
			</td>
		

			<td valign='top'>
				This class excludes:- management of pension funds, see 66.30- compulsory social security schemes, see 84.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399231
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				66 
			</td>
		

			<td valign='top'>
				K 
			</td>
		

			<td valign='top'>
				Activities auxiliary to financial services and insurance activities 
			</td>
		

			<td valign='top'>
				66 
			</td>
		

			<td valign='top'>
				This division includes the provision of services involved in or closely related to financial service activities, but not themselves providing financial services. The primary breakdown of this division is according to the type of financial transaction or funding served. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399232
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				66.1 
			</td>
		

			<td valign='top'>
				66 
			</td>
		

			<td valign='top'>
				Activities auxiliary to financial services, except insurance and pension funding 
			</td>
		

			<td valign='top'>
				661 
			</td>
		

			<td valign='top'>
				This group includes the furnishing of physical or electronic marketplaces for the purpose of facilitating the buying and selling of stocks, stock options, bonds or commodity contracts. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399233
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.11 
			</td>
		

			<td valign='top'>
				66.1 
			</td>
		

			<td valign='top'>
				Administration of financial markets 
			</td>
		

			<td valign='top'>
				6611 
			</td>
		

			<td valign='top'>
				This class includes the operation and supervision of financial markets other than by public authorities, such as:- commodity contracts exchanges- futures commodity contracts exchanges- securities exchanges- stock exchanges- stock or commodity options exchanges 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399234
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.12 
			</td>
		

			<td valign='top'>
				66.1 
			</td>
		

			<td valign='top'>
				Security and commodity contracts brokerage 
			</td>
		

			<td valign='top'>
				6612 
			</td>
		

			<td valign='top'>
				This class includes:- dealing in financial markets on behalf of others (e.g. stock broking) and related activities- securities brokerage- commodity contracts brokerage - activities of bureaux de change etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Buying and selling wind farms as investment projects- Currency trading on the Internet 
			</td>
		

			<td valign='top'>
				This class excludes:- dealing in markets on own account, see 64.99- portfolio management, on a fee or contract basis, see 66.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399235
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.19 
			</td>
		

			<td valign='top'>
				66.1 
			</td>
		

			<td valign='top'>
				Other activities auxiliary to financial services, except insurance and pension funding 
			</td>
		

			<td valign='top'>
				6619 
			</td>
		

			<td valign='top'>
				This class includes activities auxiliary to financial service activities not elsewhere classified, such as:- financial transaction processing and settlement activities, including for credit card transactions- investment advisory services - activities of mortgage advisers and brokers 
			</td>
		

			<td valign='top'>
				This class also includes:- trustee, fiduciary and custody services on a fee or contract basis 
			</td>
		

			<td valign='top'>
				- Tax refund offices- Cash-pooling activities- Investment crowd funding for start-ups 
			</td>
		

			<td valign='top'>
				This class excludes:- activities of insurance agents and brokers, see 66.22- management of funds, see 66.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399236
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				66.2 
			</td>
		

			<td valign='top'>
				66 
			</td>
		

			<td valign='top'>
				Activities auxiliary to insurance and pension funding 
			</td>
		

			<td valign='top'>
				662 
			</td>
		

			<td valign='top'>
				This group includes acting as agents (i.e. brokers) in selling annuities and insurance policies or provide other employee benefits and insurance and pension related services such as claims adjustment and third party administration. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399237
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.21 
			</td>
		

			<td valign='top'>
				66.2 
			</td>
		

			<td valign='top'>
				Risk and damage evaluation 
			</td>
		

			<td valign='top'>
				6621 
			</td>
		

			<td valign='top'>
				This class includes the provision of administration services of insurance, such as assessing and settling insurance claims, such as:- assessing insurance claims  • claims adjusting  • risk assessing  • risk and damage evaluation  • average and loss adjusting- settling insurance claims 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Insurance claims settlement 
			</td>
		

			<td valign='top'>
				This class excludes:- appraisal of real estate, see 68.31- appraisal for other purposes, see 74.90- investigation activities, see 80.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399238
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.22 
			</td>
		

			<td valign='top'>
				66.2 
			</td>
		

			<td valign='top'>
				Activities of insurance agents and brokers 
			</td>
		

			<td valign='top'>
				6622 
			</td>
		

			<td valign='top'>
				This class includes:- activities of insurance agents and brokers (insurance intermediaries) in selling, negotiating or soliciting of annuities and insurance and reinsurance policies 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Health plan management agents and brokers 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399239
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.29 
			</td>
		

			<td valign='top'>
				66.2 
			</td>
		

			<td valign='top'>
				Other activities auxiliary to insurance and pension funding 
			</td>
		

			<td valign='top'>
				6629 
			</td>
		

			<td valign='top'>
				This class includes:- activities involved in or closely related to insurance and pension funding (except financial intermediation, claims adjusting and activities of insurance agents):  • salvage administration  • actuarial services 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Administration of Insurance Guarantee Fund (such as green card agreement) 
			</td>
		

			<td valign='top'>
				This class excludes:- marine salvage activities, see 52.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399240
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				66.3 
			</td>
		

			<td valign='top'>
				66 
			</td>
		

			<td valign='top'>
				Fund management activities 
			</td>
		

			<td valign='top'>
				663 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399241
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				66.30 
			</td>
		

			<td valign='top'>
				66.3 
			</td>
		

			<td valign='top'>
				Fund management activities 
			</td>
		

			<td valign='top'>
				6630 
			</td>
		

			<td valign='top'>
				This class includes portfolio and fund management activities on a fee or contract basis, for individuals, businesses and others, such as:- management of mutual funds- management of other investment funds- management of pension funds 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399242
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				L 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				REAL ESTATE ACTIVITIES 
			</td>
		

			<td valign='top'>
				L 
			</td>
		

			<td valign='top'>
				This section includes acting as lessors, agents and/or brokers in one or more of the following: selling or buying real estate, rental real estate, providing other real estate services such as appraising real estate or acting as real estate escrow agents. Activities in this section may be carried out on own or leased property and may be done on a fee or contract basis. This section includes real estate property managers. 
			</td>
		

			<td valign='top'>
				Also included is the building of structures, combined with maintaining ownership or leasing of such structures. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399243
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				68 
			</td>
		

			<td valign='top'>
				L 
			</td>
		

			<td valign='top'>
				Real estate activities 
			</td>
		

			<td valign='top'>
				68 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399244
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				68.1 
			</td>
		

			<td valign='top'>
				68 
			</td>
		

			<td valign='top'>
				Buying and selling of own real estate 
			</td>
		

			<td valign='top'>
				681 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399245
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				68.10 
			</td>
		

			<td valign='top'>
				68.1 
			</td>
		

			<td valign='top'>
				Buying and selling of own real estate 
			</td>
		

			<td valign='top'>
				6810 
			</td>
		

			<td valign='top'>
				This class includes:- buying and selling of self-owned real estate:  • apartment buildings and dwellings  • non-residential buildings, including exhibition halls, self-storage facilities, malls and shopping centres  • land 
			</td>
		

			<td valign='top'>
				This class also includes- subdividing real estate into lots, without land improvement 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- development of building projects for sale, see 41.10- subdividing and improving of land, see 42.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399246
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				68.2 
			</td>
		

			<td valign='top'>
				68 
			</td>
		

			<td valign='top'>
				Rental and operating of own or leased real estate 
			</td>
		

			<td valign='top'>
				681 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399247
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				68.20 
			</td>
		

			<td valign='top'>
				68.2 
			</td>
		

			<td valign='top'>
				Rental and operating of own or leased real estate 
			</td>
		

			<td valign='top'>
				6810 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operating of self-owned or leased real estate:  • apartment buildings and dwellings  • non-residential buildings, including exhibition halls, self-storage facilities  • land- provision of homes and furnished or unfurnished flats or apartments for more permanent use, typically on a monthly or annual basis 
			</td>
		

			<td valign='top'>
				This class also includes:- development of building projects for own operation- operation of residential mobile home sites 
			</td>
		

			<td valign='top'>
				- Letting of roofs for solar power installations- Rental of photovoltaic power plants 
			</td>
		

			<td valign='top'>
				This class excludes:- operation of hotels, suite hotels, holiday homes, rooming houses, campgrounds, trailer parks and other non-residential or short-stay accommodation places, see division 55 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399248
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				68.3 
			</td>
		

			<td valign='top'>
				68 
			</td>
		

			<td valign='top'>
				Real estate activities on a fee or contract basis 
			</td>
		

			<td valign='top'>
				682 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399249
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				68.31 
			</td>
		

			<td valign='top'>
				68.3 
			</td>
		

			<td valign='top'>
				Real estate agencies 
			</td>
		

			<td valign='top'>
				6820 
			</td>
		

			<td valign='top'>
				This class includes the provision of real estate activities by real estate agencies:- intermediation in buying, selling and rental of real estate on a fee or contract basis- advisory activities and appraisal services in connection to buying, selling and rental of real estate, on a fee or contract basis- real estate escrow agents activities 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- legal activities, see 69.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399250
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				68.32 
			</td>
		

			<td valign='top'>
				68.3 
			</td>
		

			<td valign='top'>
				Management of real estate on a fee or contract basis 
			</td>
		

			<td valign='top'>
				6820 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- rent-collecting agencies 
			</td>
		

			<td valign='top'>
				- Management by owners of co-owned dwelling facilities 
			</td>
		

			<td valign='top'>
				This class excludes:- legal activities, see 69.10- facilities support services (combination of services such as general interior cleaning, maintenance and making minor repairs, trash disposal, guard and security), see 81.10- management of facilities, such as military bases, prisons and other facilities (except computer facilities management), see 81.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399251
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				M 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				PROFESSIONAL, SCIENTIFIC AND TECHNICAL ACTIVITIES 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				This section includes specialised professional, scientific and technical activities. These activities require a high degree of training, and make specialised knowledge and skills available to users. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399252
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				69 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Legal and accounting activities 
			</td>
		

			<td valign='top'>
				69 
			</td>
		

			<td valign='top'>
				This division includes legal representation of one party's interest against another party, whether or not before courts or other judicial bodies by, or under supervision of, persons who are members of the bar, such as advice and representation in civil cases, advice and representation in criminal actions, advice and representation in connection with labour disputes. 
			</td>
		

			<td valign='top'>
				It also includes preparation of legal documents such as articles of incorporation, partnership agreements or similar documents in connection with company formation, patents and copyrights, preparation of deeds, wills, trusts, etc. as well as other activities of notaries public, civil law notaries, bailiffs, arbitrators, examiners and referees.It also includes accounting and bookkeeping services such as auditing of accounting records, preparing financial statements and bookkeeping. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399253
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				69.1 
			</td>
		

			<td valign='top'>
				69 
			</td>
		

			<td valign='top'>
				Legal activities 
			</td>
		

			<td valign='top'>
				691 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399254
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				69.10 
			</td>
		

			<td valign='top'>
				69.1 
			</td>
		

			<td valign='top'>
				Legal activities 
			</td>
		

			<td valign='top'>
				6910 
			</td>
		

			<td valign='top'>
				This class includes:- legal representation of one party's interest against another party, whether or not before courts or other judicial bodies by, or under supervision of, persons who are members of the bar:  • advice and representation in civil cases  • advice and representation in criminal cases  • advice and representation in connection with labour disputes- general counselling and advising, preparation of legal documents:  • articles of incorporation, partnership agreements or similar documents in connection with company formation  • patents and copyrights  • preparation of deeds, wills, trusts etc.- other activities of notaries public, civil law notaries, bailiffs, arbitrators, examiners and referees 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- law court activities, see 84.23 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399255
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				69.2 
			</td>
		

			<td valign='top'>
				69 
			</td>
		

			<td valign='top'>
				Accounting, bookkeeping and auditing activities; tax consultancy 
			</td>
		

			<td valign='top'>
				692 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399256
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				69.20 
			</td>
		

			<td valign='top'>
				69.2 
			</td>
		

			<td valign='top'>
				Accounting, bookkeeping and auditing activities; tax consultancy 
			</td>
		

			<td valign='top'>
				6920 
			</td>
		

			<td valign='top'>
				This class includes:- recording of commercial transactions from businesses or others- preparation or auditing of financial accounts- examination of accounts and certification of their accuracy- preparation of personal and business income tax returns- advisory activities and representation on behalf of clients before tax authorities 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Duty and tax tariff calculation service activities 
			</td>
		

			<td valign='top'>
				This class excludes:- data-processing and tabulation activities, see 63.11- management consultancy on accounting systems, budgetary control procedures, see 70.22- bill collection, see 82.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399257
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				70 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Activities of head offices; management consultancy activities 
			</td>
		

			<td valign='top'>
				70 
			</td>
		

			<td valign='top'>
				This division includes the provision of advice and assistance to businesses and other organisations on management issues, such as strategic and organisational planning; financial planning and budgeting; marketing objectives and policies; human resource policies, practices, and planning; production scheduling; and control planning. 
			</td>
		

			<td valign='top'>
				It also includes the overseeing and managing of other units of the same company or enterprise, i.e. the activities of head offices. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399258
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				70.1 
			</td>
		

			<td valign='top'>
				70 
			</td>
		

			<td valign='top'>
				Activities of head offices 
			</td>
		

			<td valign='top'>
				701 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399259
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				70.10 
			</td>
		

			<td valign='top'>
				70.1 
			</td>
		

			<td valign='top'>
				Activities of head offices 
			</td>
		

			<td valign='top'>
				7010 
			</td>
		

			<td valign='top'>
				This class includes the overseeing and managing of other units of the company or enterprise; undertaking the strategic or organisational planning and decision making role of the company or enterprise; exercising operational control and managing the day-to-day operations of their related units.This class includes activities of:- head offices- centralised administrative offices- corporate offices- district and regional offices- subsidiary management offices 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of holding companies, not engaged in managing, see 64.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399260
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				70.2 
			</td>
		

			<td valign='top'>
				70 
			</td>
		

			<td valign='top'>
				Management consultancy activities 
			</td>
		

			<td valign='top'>
				702 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399261
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				70.21 
			</td>
		

			<td valign='top'>
				70.2 
			</td>
		

			<td valign='top'>
				Public relations and communication activities 
			</td>
		

			<td valign='top'>
				7020 
			</td>
		

			<td valign='top'>
				This class includes the provision of advice, guidance and operational assistance, including lobbying activities, to businesses and other organisations on public relations and communication. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- advertising agencies and media representation services, see 73.1- market research and public opinion polling, see 73.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399262
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				70.22 
			</td>
		

			<td valign='top'>
				70.2 
			</td>
		

			<td valign='top'>
				Business and other management consultancy activities 
			</td>
		

			<td valign='top'>
				7020 
			</td>
		

			<td valign='top'>
				This class includes the provision of advice, guidance and operational assistance to businesses and other organisations on management issues, such as corporate strategic and organisational planning, business process reengineering, change management, cost reduction and other financial issues; marketing objectives and policies; human resource policies, practices and planning; compensation and retirement strategies; production scheduling and control planning.This provision of business services may include advice, guidance or operational assistance to businesses and the public service regarding:- design of accounting methods or procedures, cost accounting programmes, budgetary control procedures- advice and help to businesses and public services in planning, organisation, efficiency and control, management information etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Provision of business and professionals guidance- Filling out the papers and attaching the supporting documents required in relation to a public procurement procedure- Business establishing and start-up service activities 
			</td>
		

			<td valign='top'>
				This class excludes:- design of computer software for accounting systems, see 62.01- legal advice and representation, see 69.10- accounting, bookkeeping and auditing activities, tax consulting, see 69.20- architectural and engineering advisory activities, see 71.11, 71.12- environmental, agronomy, security and similar consulting activities, see 74.90- executive placement or search consulting activities, see 78.10- educational consulting activities, see 85.60 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399263
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				71 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Architectural and engineering activities; technical testing and analysis 
			</td>
		

			<td valign='top'>
				71 
			</td>
		

			<td valign='top'>
				This division includes the provision of architectural services, engineering services, drafting services, building inspection services and surveying and mapping services. 
			</td>
		

			<td valign='top'>
				It also includes the performance of physical, chemical, and other analytical testing services. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399264
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				71.1 
			</td>
		

			<td valign='top'>
				71 
			</td>
		

			<td valign='top'>
				Architectural and engineering activities and related technical consultancy 
			</td>
		

			<td valign='top'>
				711 
			</td>
		

			<td valign='top'>
				This group includes the provision of architectural services, engineering services, drafting services, building inspection services and surveying and mapping services and the like. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399265
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				71.11 
			</td>
		

			<td valign='top'>
				71.1 
			</td>
		

			<td valign='top'>
				Architectural activities 
			</td>
		

			<td valign='top'>
				7110 
			</td>
		

			<td valign='top'>
				This class includes:- architectural consulting activities:  • building design and drafting  • town and city planning and landscape architecture 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of computer consultants, see 62.02, 62.09- interior decorating, see 74.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399266
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				71.12 
			</td>
		

			<td valign='top'>
				71.1 
			</td>
		

			<td valign='top'>
				Engineering activities and related technical consultancy 
			</td>
		

			<td valign='top'>
				7110 
			</td>
		

			<td valign='top'>
				This class includes:- engineering design (i.e. applying physical laws and principles of engineering in the design of machines, materials, instruments, structures, processes and systems) and consulting activities for:  • machinery, industrial processes and industrial plant  • projects involving civil engineering, hydraulic engineering, traffic engineering  • water management projects  • projects elaboration and realisation relative to electrical and electronic engineering, mining engineering, chemical engineering, mechanical, industrial and systems engineering, safety engineering- elaboration of projects using air conditioning, refrigeration, sanitary and pollution control engineering, acoustical engineering etc.- geophysical, geologic and seismic surveying- geodetic surveying activities:  • land and boundary surveying activities  • hydrologic surveying activities  • subsurface surveying activities  • cartographic and spatial information activities 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Updating of navigation maps- Go-kart track design activities 
			</td>
		

			<td valign='top'>
				This class excludes:- test drilling in connection with mining operations, see 09.10, 09.90- development or publishing of associated software, see 58.29, 62.01- activities of computer consultants, see 62.02, 62.09- technical testing, see 71.20- research and development activities related to engineering, see 72.19- industrial design, see 74.10- aerial photography, see 74.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399267
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				71.2 
			</td>
		

			<td valign='top'>
				71 
			</td>
		

			<td valign='top'>
				Technical testing and analysis 
			</td>
		

			<td valign='top'>
				712 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399268
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				71.20 
			</td>
		

			<td valign='top'>
				71.2 
			</td>
		

			<td valign='top'>
				Technical testing and analysis 
			</td>
		

			<td valign='top'>
				7120 
			</td>
		

			<td valign='top'>
				This class includes the performance of physical, chemical and other analytical testing of all types of materials and products, such as:  • acoustics and vibration testing  • testing of composition and purity of minerals etc.  • testing activities in the field of food hygiene, including veterinary testing and control in relation to food production  • testing of physical characteristics and performance of materials, such as strength, thickness, durability, radioactivity etc.  • qualification and reliability testing  • performance testing of complete machinery: motors, automobiles, electronic equipment etc.  • radiographic testing of welds and joints  • failure analysis  • testing and measuring of environmental indicators: air and water pollution etc.- certification of products, including consumer goods, motor vehicles, aircraft, pressurised containers, nuclear plants etc.- periodic road-safety testing of motor vehicles- testing with use of models or mock-ups (e.g. of aircraft, ships, dams etc.)- operation of police laboratories 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Archaeobotanical analyses- Inspection of oilfield pipes- Product origin and quality assessment activities- Think tanks (policy institutes) conducting research and experimental development on social sciences and humanities- Excavation, digging on archaeological sites and research 
			</td>
		

			<td valign='top'>
				This class excludes:- testing of animal specimens, see 75.00- diagnostic imaging, testing and analysis of medical and dental specimens, see 86 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399269
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				72 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Scientific research and development 
			</td>
		

			<td valign='top'>
				72 
			</td>
		

			<td valign='top'>
				This division includes the activities of three types of research and development: 1) basic research: experimental or theoretical work undertaken primarily to acquire new knowledge of the underlying foundations of phenomena and observable facts, without particular application or use in view, 2) applied research: original investigation undertaken in order to acquire new knowledge, directed primarily towards a specific practical aim or objective and 3) experimental development: systematic work, drawing on existing knowledge gained from research and/or practical experience, directed to producing new materials, products and devices, to installing new processes, systems and services, and to improving substantially those already produced or installed.Research and experimental development activities in this division are subdivided into two categories: natural sciences and engineering; social sciences and the humanities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- market research, see 73.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399270
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				72.1 
			</td>
		

			<td valign='top'>
				72 
			</td>
		

			<td valign='top'>
				Research and experimental development on natural sciences and engineering 
			</td>
		

			<td valign='top'>
				721 
			</td>
		

			<td valign='top'>
				This group comprises basic research, applied research, experimental development in natural sciences and engineering. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399271
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				72.11 
			</td>
		

			<td valign='top'>
				72.1 
			</td>
		

			<td valign='top'>
				Research and experimental development on biotechnology 
			</td>
		

			<td valign='top'>
				7210 
			</td>
		

			<td valign='top'>
				This class includes research and experimental development on biotechnology:- DNA/RNA: genomics, pharmacogenomics, gene probes, genetic engineering, DNA/RNA sequencing/synthesis/amplification, gene expression profiling, and use of antisense technology- proteins and other molecules: sequencing/synthesis/engineering of proteins and peptides (including large molecule hormones); improved delivery methods for large molecule drugs; proteomics, protein isolation and purification, signalling, identification of cell receptors- cell and tissue culture and engineering: cell/tissue culture, tissue engineering (including tissue scaffolds and biomedical engineering), cellular fusion, vaccine/immune stimulants, embryo manipulation- process biotechnology techniques: fermentation using bioreactors, bioprocessing, bioleaching, biopulping, biobleaching, biodesulphurisation, bioremediation, biofiltration and phytoremediation- gene and RNA vectors: gene therapy, viral vectors- bioinformatics: construction of databases on genomes, protein sequences; modelling complex biological processes, including systems biology- nanobiotechnology: applies the tools and processes of nano/microfabrication to build devices for studying biosystems and applications in drug delivery, diagnostics etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399272
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				72.19 
			</td>
		

			<td valign='top'>
				72.1 
			</td>
		

			<td valign='top'>
				Other research and experimental development on natural sciences and engineering 
			</td>
		

			<td valign='top'>
				7210 
			</td>
		

			<td valign='top'>
				This class includes:- research and experimental development on natural science and engineering other than biotechnological research and experimental development:  • research and development on natural sciences  • research and development on engineering and technology  • research and development on medical sciences  • research and development on agricultural sciences  • interdisciplinary research and development, predominantly on natural sciences and engineering 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399273
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				72.2 
			</td>
		

			<td valign='top'>
				72 
			</td>
		

			<td valign='top'>
				Research and experimental development on social sciences and humanities 
			</td>
		

			<td valign='top'>
				722 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399274
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				72.20 
			</td>
		

			<td valign='top'>
				72.2 
			</td>
		

			<td valign='top'>
				Research and experimental development on social sciences and humanities 
			</td>
		

			<td valign='top'>
				7220 
			</td>
		

			<td valign='top'>
				This class includes:- research and development on social sciences- research and development on humanities- interdisciplinary research and development, predominantly on social sciences and humanities 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				Excavation, digging on archaeological sites and research 
			</td>
		

			<td valign='top'>
				This class excludes:- market research, see 73.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399275
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				73 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Advertising and market research 
			</td>
		

			<td valign='top'>
				73 
			</td>
		

			<td valign='top'>
				This division includes the creation of advertising campaigns and placement of such advertising in periodicals, newspapers, radio and television, or other media as well as the design of display structures and sites. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399276
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				73.1 
			</td>
		

			<td valign='top'>
				73 
			</td>
		

			<td valign='top'>
				Advertising 
			</td>
		

			<td valign='top'>
				731 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399277
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				73.11 
			</td>
		

			<td valign='top'>
				73.1 
			</td>
		

			<td valign='top'>
				Advertising agencies 
			</td>
		

			<td valign='top'>
				7310 
			</td>
		

			<td valign='top'>
				This class includes the provision of a full range of advertising services (i.e., through in-house capabilities or subcontracting), including advice, creative services, production of advertising material, and buying. It includes:- creation and realisation of advertising campaigns:  • creating and placing advertising in newspapers, periodicals, radio, television, the Internet and other media  • creating and placing of outdoor advertising, e.g. billboards, panels, bulletins and frames, window dressing, showroom design, car and bus carding etc.  • aerial advertising  • distribution or delivery of advertising material or samples  • creation of stands and other display structures and sites- conducting marketing campaigns and other advertising services aimed at attracting and retaining customers  • promotion of products  • point-of-sale marketing  • direct mail advertising  • marketing consulting 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- publishing of advertising material, see 58.19- production of commercial messages television and film, see 59.11- production of commercial messages for radio, see 59.20- market research, see 73.20- advertising photography, see 74.20- convention and trade show organisers, see 82.30- mailing activities, see 82.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399278
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				73.12 
			</td>
		

			<td valign='top'>
				73.1 
			</td>
		

			<td valign='top'>
				Media representation 
			</td>
		

			<td valign='top'>
				7310 
			</td>
		

			<td valign='top'>
				This class includes- media representation, i.e. sale or re-sale of time and space for various media soliciting advertising 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Advertising space sale via intermediaries 
			</td>
		

			<td valign='top'>
				This class excludes:- sale of advertising time or space directly by owners of the time or space (publishers etc.), see the corresponding activity class - public-relations activities, see 70.21 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399279
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				73.2 
			</td>
		

			<td valign='top'>
				73 
			</td>
		

			<td valign='top'>
				Market research and public opinion polling 
			</td>
		

			<td valign='top'>
				732 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399280
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				73.20 
			</td>
		

			<td valign='top'>
				73.2 
			</td>
		

			<td valign='top'>
				Market research and public opinion polling 
			</td>
		

			<td valign='top'>
				7320 
			</td>
		

			<td valign='top'>
				This class includes:- investigation into market potential, awareness, acceptance and familiarity of goods and services and buying habits of consumers for the purpose of sales promotion and development of new goods and services, including statistical analyses of the results- investigation into collective opinions of the public about political, economic and social issues and statistical analysis thereof 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Undercover market research, to assess service level by visiting or calling business- Testing of website usability 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399281
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				74 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Other professional, scientific and technical activities 
			</td>
		

			<td valign='top'>
				74 
			</td>
		

			<td valign='top'>
				This division includes the provision of professional scientific and technical services (except legal and accounting activities; architecture and engineering activities; technical testing and analysis; management and management consultancy activities; research and development and advertising activities). 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399282
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				74.1 
			</td>
		

			<td valign='top'>
				74 
			</td>
		

			<td valign='top'>
				Specialised design activities 
			</td>
		

			<td valign='top'>
				741 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399283
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				74.10 
			</td>
		

			<td valign='top'>
				74.1 
			</td>
		

			<td valign='top'>
				Specialised design activities 
			</td>
		

			<td valign='top'>
				7410 
			</td>
		

			<td valign='top'>
				This class includes:- fashion design related to textiles, wearing apparel, shoes, jewellery, furniture and other interior decoration and other fashion goods as well as other personal or household goods- industrial design, i.e. creating and developing designs and specifications that optimise the use, value and appearance of products, including the determination of the materials, mechanism, shape, colour and surface finishes of the product, taking into consideration human characteristics and needs, safety, market appeal in distribution, use and maintenance- activities of graphic designers- activities of interior decorators 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- design and programming of web pages, see 62.01- architectural design, see 71.11- engineering design, i.e. applying physical laws and principles of engineering in the design of machines, materials, instruments, structures, processes and systems, see 71.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399284
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				74.2 
			</td>
		

			<td valign='top'>
				74 
			</td>
		

			<td valign='top'>
				Photographic activities 
			</td>
		

			<td valign='top'>
				742 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399285
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				74.20 
			</td>
		

			<td valign='top'>
				74.2 
			</td>
		

			<td valign='top'>
				Photographic activities 
			</td>
		

			<td valign='top'>
				7420 
			</td>
		

			<td valign='top'>
				This class includes:- commercial and consumer photograph production:  • portrait photography for passports, schools, weddings etc.  • photography for commercials, publishers, fashion, real estate or tourism purposes  • aerial photography   • videotaping of events: weddings, meetings etc.- film processing:  • developing, printing and enlarging from client-taken negatives or cine-films  • film developing and photo printing laboratories   • one hour photo shops (not part of camera stores)  • mounting of slides  • copying and restoring or transparency retouching in connection with photographs- activities of photojournalists 
			</td>
		

			<td valign='top'>
				This class also includes:- microfilming of documents 
			</td>
		

			<td valign='top'>
				- Transferring content from video tapes (and similar media) to other media- Object scanning in 3D 
			</td>
		

			<td valign='top'>
				This class excludes:- processing motion picture film related to the motion picture and television industries, see 59.12- cartographic and spatial information activities, see 71.12- operation of coin-operated (self-service) photo machines, see 96.09 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399286
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				74.3 
			</td>
		

			<td valign='top'>
				74 
			</td>
		

			<td valign='top'>
				Translation and interpretation activities 
			</td>
		

			<td valign='top'>
				749 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399287
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				74.30 
			</td>
		

			<td valign='top'>
				74.3 
			</td>
		

			<td valign='top'>
				Translation and interpretation activities 
			</td>
		

			<td valign='top'>
				7490 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				- Sign language interpretation activities 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399288
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				74.9 
			</td>
		

			<td valign='top'>
				74 
			</td>
		

			<td valign='top'>
				Other professional, scientific and technical activities n.e.c. 
			</td>
		

			<td valign='top'>
				749 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399289
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				74.90 
			</td>
		

			<td valign='top'>
				74.9 
			</td>
		

			<td valign='top'>
				Other professional, scientific and technical activities n.e.c. 
			</td>
		

			<td valign='top'>
				7490 
			</td>
		

			<td valign='top'>
				This class includes a great variety of service activities generally delivered to commercial clients. It includes those activities for which more advanced professional, scientific and technical skill levels are required, but does not include ongoing, routine business functions that are generally of short duration.This class includes:- business brokerage activities, i.e. arranging for the purchase and sale of small and medium-sized businesses, including professional practices, but not including real estate brokerage- patent brokerage activities (arranging for the purchase and sale of patents)- appraisal activities other than for real estate and insurance (for antiques, jewellery, etc.)- bill auditing and freight rate information- weather forecasting activities- security consulting- agronomy consulting- environmental consulting- other technical consulting- activities of consultants other than architecture, engineering and management consultants- activities of quantity surveyors 
			</td>
		

			<td valign='top'>
				This class also includes:- activities carried out by agents and agencies on behalf of individuals usually involving the obtaining of engagements in motion picture, theatrical production or other entertainment or sports attractions and the placement of books, plays, artworks, photographs etc., with publishers, producers etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- wholesale of used motor vehicles by auctioning, see 45.1- online auction activities (retail), see 47.91- activities of auctioning houses (retail), see 47.79- activities of real estate brokers, see 68.31- bookkeeping activities, see 69.20- activities of management consultants, see 70.22- activities of architecture and engineering consultants, see 71.1- industrial and machinery design, see 71.12, 74.10- veterinary testing and control in relation to food production, see 71.20- display of advertisement and other advertising design, see 73.11- creation of stands and other display structures and sites, see 73.11- activities of convention and trade show organisers, see 82.30- activities of independent auctioneers, see 82.99- administration of loyalty programmes, see 82.99- consumer credit and debt counselling, see 88.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399290
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				75 
			</td>
		

			<td valign='top'>
				M 
			</td>
		

			<td valign='top'>
				Veterinary activities 
			</td>
		

			<td valign='top'>
				75 
			</td>
		

			<td valign='top'>
				This division includes the provision of animal health care and control activities for farm animals or pet animals. These activities are carried out by qualified veterinarians in veterinary hospitals as well as when visiting farms, kennels or homes, in own consulting and surgery rooms or elsewhere. 
			</td>
		

			<td valign='top'>
				It also includes animal ambulance activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399291
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				75.0 
			</td>
		

			<td valign='top'>
				75 
			</td>
		

			<td valign='top'>
				Veterinary activities 
			</td>
		

			<td valign='top'>
				750 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399292
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				75.00 
			</td>
		

			<td valign='top'>
				75.0 
			</td>
		

			<td valign='top'>
				Veterinary activities 
			</td>
		

			<td valign='top'>
				7500 
			</td>
		

			<td valign='top'>
				This class includes:- animal health care and control activities for farm animals- animal health care and control activities for pet animalsThese activities are carried out by qualified veterinarians when working in veterinary hospitals as well as when visiting farms, kennels or homes, in own consulting and surgery rooms or elsewhere. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of veterinary assistants or other auxiliary veterinary personnel- clinico-pathological and other diagnostic activities pertaining to animals- animal ambulance activities 
			</td>
		

			<td valign='top'>
				- Animal genetic testing activities 
			</td>
		

			<td valign='top'>
				This class excludes:- farm animal boarding activities without health care, see 01.62- sheep shearing, see 01.62- herd testing services, droving services, agistment services, poultry caponising, see 01.62- activities related to artificial insemination, see 01.62- pet animal boarding activities without health care, see 96.09 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399293
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				N 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				ADMINISTRATIVE AND SUPPORT SERVICE ACTIVITIES 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				This section includes a variety of activities that support general business operations. These activities differ from those in section M, since their primary purpose is not the transfer of specialised knowledge. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399294
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				77 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				Rental and leasing activities 
			</td>
		

			<td valign='top'>
				77 
			</td>
		

			<td valign='top'>
				This division includes the rental and leasing of tangible and non-financial intangible assets, including a wide array of tangible goods, such as automobiles, computers, consumer goods, and industrial machinery and equipment, to customers in return for a periodic rental or lease payment. It is subdivided into: (1) the rental of motor vehicles, (2) the rental of recreational and sports equipment and personal and household equipment, (3) the leasing of other machinery and equipment of the kind often used for business operations, including other transport equipment and (4) the leasing of intellectual property products and similar products. Only the provision of operating leases is included in this division. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- financial leasing, see 64.91- rental of real estate, see section L- rental of equipment with operator, see corresponding classes according to activities carried out with this equipment, e.g. construction (section F), transportation (section H) 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399295
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				77.1 
			</td>
		

			<td valign='top'>
				77 
			</td>
		

			<td valign='top'>
				Rental and leasing of motor vehicles 
			</td>
		

			<td valign='top'>
				771 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399296
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.11 
			</td>
		

			<td valign='top'>
				77.1 
			</td>
		

			<td valign='top'>
				Rental and leasing of cars and light motor vehicles 
			</td>
		

			<td valign='top'>
				7710 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of the following types of vehicles:  • passenger cars and other light motor vehicles (with a weight not exceeding 3,5 tons) without driver 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental or leasing of cars or light motor vehicles with driver, see 49.32, 49.39 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399297
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.12 
			</td>
		

			<td valign='top'>
				77.1 
			</td>
		

			<td valign='top'>
				Rental and leasing of trucks 
			</td>
		

			<td valign='top'>
				7710 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of the following types of vehicles:  • trucks, utility trailers and heavy motor vehicles (with a weight exceeding 3,5 tons)  • recreational vehicles 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental or leasing of heavy goods vehicles or trucks with driver, see 49.41 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399298
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				77.2 
			</td>
		

			<td valign='top'>
				77 
			</td>
		

			<td valign='top'>
				Rental and leasing of personal and household goods 
			</td>
		

			<td valign='top'>
				772 
			</td>
		

			<td valign='top'>
				This group includes the rental of personal and household goods as well as rental of recreational and sports equipment and video tapes. Activities generally include short-term rental of goods although in some instances, the goods may be leased for longer periods of time. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399299
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.21 
			</td>
		

			<td valign='top'>
				77.2 
			</td>
		

			<td valign='top'>
				Rental and leasing of recreational and sports goods 
			</td>
		

			<td valign='top'>
				7721 
			</td>
		

			<td valign='top'>
				This class includes rental of recreational and sports equipment:- pleasure boats, canoes, sailboats- bicycles- beach chairs and umbrellas- other sports equipment- skis 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Rental of yachts without operator 
			</td>
		

			<td valign='top'>
				This class excludes:- rental of pleasure boats and sailing boats with crew, see 50.10, 50.30- rental of video tapes and disks, see 77.22- rental of other personal and household goods n.e.c., see 77.29- rental of leisure and pleasure equipment as an integral part of recreational facilities, see 93.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399300
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.22 
			</td>
		

			<td valign='top'>
				77.2 
			</td>
		

			<td valign='top'>
				Rental of video tapes and disks 
			</td>
		

			<td valign='top'>
				7722 
			</td>
		

			<td valign='top'>
				This class includes:- rental of video tapes, records, CDs, DVDs etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399301
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.29 
			</td>
		

			<td valign='top'>
				77.2 
			</td>
		

			<td valign='top'>
				Rental and leasing of other personal and household goods 
			</td>
		

			<td valign='top'>
				7729 
			</td>
		

			<td valign='top'>
				This class includes:- rental of all kinds of household or personal goods, to households or industries (except recreational and sports equipment):  • textiles, wearing apparel and footwear  • furniture, pottery and glass, kitchen and tableware, electrical appliances and house wares  • jewellery, musical instruments, scenery and costumes  • books, journals and magazines  • machinery and equipment used by amateurs or as a hobby e.g. tools for home repairs  • flowers and plants  • electronic equipment for household use 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of cars, trucks, trailers and recreational vehicles without driver, see 77.1- rental of recreational and sports goods, see 77.21- rental of video tapes and disks, see 77.22- rental of office furniture, see 77.33- rental of motorcycles and caravans without driver, see 77.39- provision of linen, work uniforms and related items by laundries, see 96.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399302
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				77 
			</td>
		

			<td valign='top'>
				Rental and leasing of other machinery, equipment and tangible goods 
			</td>
		

			<td valign='top'>
				773 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399303
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.31 
			</td>
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				Rental and leasing of agricultural machinery and equipment 
			</td>
		

			<td valign='top'>
				7730 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of agricultural and forestry machinery and equipment without operator:  • rental of products produced by class 28.30, such as agricultural tractors etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of agricultural and forestry machinery or equipment with operator, see 01.61, 02.40 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399304
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.32 
			</td>
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				Rental and leasing of construction and civil engineering machinery and equipment 
			</td>
		

			<td valign='top'>
				7730 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of construction and civil engineering machinery and equipment without operator:  • crane lorries  • scaffolds and work platforms, without erection and dismantling 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of construction and civil engineering machinery or equipment with operator, see division 43 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399305
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.33 
			</td>
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				Rental and leasing of office machinery and equipment (including computers) 
			</td>
		

			<td valign='top'>
				7730 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of office machinery and equipment without operator:  • computers and computer peripheral equipment  • duplicating machines, typewriters and word-processing machines  • accounting machinery and equipment: cash registers, electronic calculators etc.  • office furniture 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399306
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.34 
			</td>
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				Rental and leasing of water transport equipment 
			</td>
		

			<td valign='top'>
				7730 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of water-transport equipment without operator:   • commercial boats and ships 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of water-transport equipment with operator, see division 50- rental of pleasure boats, see 77.21 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399307
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.35 
			</td>
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				Rental and leasing of air transport equipment 
			</td>
		

			<td valign='top'>
				7730 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing of air transport equipment without operator:  • airplanes  • hot-air balloons 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of air-transport equipment with operator, see division 51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399308
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.39 
			</td>
		

			<td valign='top'>
				77.3 
			</td>
		

			<td valign='top'>
				Rental and leasing of other machinery, equipment and tangible goods n.e.c. 
			</td>
		

			<td valign='top'>
				7730 
			</td>
		

			<td valign='top'>
				This class includes:- rental and operational leasing, without operator, of other machinery and equipment that are generally used as capital goods by industries:  • engines and turbines  • machine tools  • mining and oilfield equipment  • professional radio, television and communication equipment  • motion picture production equipment  • measuring and controlling equipment  • other scientific, commercial and industrial machinery- rental and operational leasing of land-transport equipment (other than motor vehicles) without drivers:  • motorcycles, caravans and campers etc.  • railroad vehicles 
			</td>
		

			<td valign='top'>
				This class also includes:- rental of accommodation or office containers- rental of animals (e.g. herds, race horses)- rental of containers- rental of pallets 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of bicycles, see 77.21- rental of agricultural and forestry machinery and equipment, see 77.31- rental of construction and civil engineering machinery and equipment, see 77.32- rental of office machinery and equipment, including computers, see 77.33 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399309
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				77.4 
			</td>
		

			<td valign='top'>
				77 
			</td>
		

			<td valign='top'>
				Leasing of intellectual property and similar products, except copyrighted works 
			</td>
		

			<td valign='top'>
				774 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399310
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				77.40 
			</td>
		

			<td valign='top'>
				77.4 
			</td>
		

			<td valign='top'>
				Leasing of intellectual property and similar products, except copyrighted works 
			</td>
		

			<td valign='top'>
				7740 
			</td>
		

			<td valign='top'>
				This class includes the activities of allowing others to use intellectual property products and similar products for which a royalty payment or licensing fee is paid to the owner of the product (i.e. the asset holder). The leasing of these products can take various forms, such as permission for reproduction, use in subsequent processes or products, operating businesses under a franchise etc. The current owners may or may not have created these products.This class includes:- leasing of intellectual property products (except copyrighted works, such as books or software)- receiving royalties or licensing fees for the use of:  • patented entities  • trademarks or service marks  • brand names  • mineral exploration and evaluation  • franchise agreements 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Lease or sale of scientific research results 
			</td>
		

			<td valign='top'>
				This class excludes:- acquisition of rights and publishing, see divisions 58 and 59- producing, reproducing and distributing copyrighted works (books, software, film), see divisions 58, 59- leasing of real estate, see 68.20- leasing of tangible products (assets), see groups 77.1, 77.2, 77.3 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399311
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				78 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				Employment activities 
			</td>
		

			<td valign='top'>
				78 
			</td>
		

			<td valign='top'>
				This division includes activities of listing employment vacancies and referring or placing applicants for employment, where the individuals referred or placed are not employees of the employment agencies, supplying workers to clients' businesses for limited periods of time to supplement the working force of the client, and the activities of providing other human resources.This division includes:- executive search and placement activities- activities of theatrical casting agencies 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- activities of agents for individual artists, see 74.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399312
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				78.1 
			</td>
		

			<td valign='top'>
				78 
			</td>
		

			<td valign='top'>
				Activities of employment placement agencies 
			</td>
		

			<td valign='top'>
				781 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399313
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				78.10 
			</td>
		

			<td valign='top'>
				78.1 
			</td>
		

			<td valign='top'>
				Activities of employment placement agencies 
			</td>
		

			<td valign='top'>
				7810 
			</td>
		

			<td valign='top'>
				This class includes listing employment vacancies and referring or placing applicants for employment, where the individuals referred or placed are not employees of the employment agencies.This class includes:- personnel search, selection referral and placement activities, including executive placement and search activities- activities of casting agencies and bureaus, such as theatrical casting agencies- activities of on-line employment placement agencies 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Sports talent scout 
			</td>
		

			<td valign='top'>
				This class excludes:- activities of personal theatrical or artistic agents or agencies, see 74.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399314
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				78.2 
			</td>
		

			<td valign='top'>
				78 
			</td>
		

			<td valign='top'>
				Temporary employment agency activities 
			</td>
		

			<td valign='top'>
				782 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399315
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				78.20 
			</td>
		

			<td valign='top'>
				78.2 
			</td>
		

			<td valign='top'>
				Temporary employment agency activities 
			</td>
		

			<td valign='top'>
				7820 
			</td>
		

			<td valign='top'>
				This class includes the activities of supplying workers to clients' businesses for limited periods of time to temporarily replace or supplement the working force of the client, where the individuals provided are employees of the temporary help service unit. However, units classified here do not provide direct supervision of their employees at the clients' work sites. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399316
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				78.3 
			</td>
		

			<td valign='top'>
				78 
			</td>
		

			<td valign='top'>
				Other human resources provision 
			</td>
		

			<td valign='top'>
				783 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399317
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				78.30 
			</td>
		

			<td valign='top'>
				78.3 
			</td>
		

			<td valign='top'>
				Other human resources provision 
			</td>
		

			<td valign='top'>
				7830 
			</td>
		

			<td valign='top'>
				This class includes the activities of providing human resources for client businesses. The units classified here represent the employer of record for the employees on matters relating to payroll, taxes, and other fiscal and human resource issues, but they are not responsible for direction and supervision of employees.The provision of human resources is typically done on a long-term or permanent basis and the units classified here perform a wide range of human resource and personnel management duties associated with this provision. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- provision of human resources functions together with supervision or running of the business, see the class in the respective economic activity of that business- provision of human resources to temporarily replace or supplement the workforce of the client, see 78.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399318
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				79 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				Travel agency, tour operator and other reservation service and related activities 
			</td>
		

			<td valign='top'>
				79 
			</td>
		

			<td valign='top'>
				This division includes the activity of agencies, primarily engaged in selling travel, tour, transportation and accommodation services to the general public and commercial clients and the activity of arranging and assembling tours that are sold through travel agencies or directly by agents such as tour operators; and other travel-related services including reservation services. 
			</td>
		

			<td valign='top'>
				The activities of tourist guides and tourism promotion activities are also included. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399319
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				79.1 
			</td>
		

			<td valign='top'>
				79 
			</td>
		

			<td valign='top'>
				Travel agency and tour operator activities 
			</td>
		

			<td valign='top'>
				791 
			</td>
		

			<td valign='top'>
				This group includes the activities of agencies, primarily engaged in selling travel, tour, transportation and accommodation services to the general public and commercial clients and the activity of arranging and assembling tours that are sold through travel agencies or directly by agents such as tour operators. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399320
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				79.11 
			</td>
		

			<td valign='top'>
				79.1 
			</td>
		

			<td valign='top'>
				Travel agency activities 
			</td>
		

			<td valign='top'>
				7911 
			</td>
		

			<td valign='top'>
				This class includes:- activities of agencies, primarily engaged in selling travel, tour, transportation and accommodation services on a wholesale or retail basis to the general public and commercial clients 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399321
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				79.12 
			</td>
		

			<td valign='top'>
				79.1 
			</td>
		

			<td valign='top'>
				Tour operator activities 
			</td>
		

			<td valign='top'>
				7912 
			</td>
		

			<td valign='top'>
				This class includes:- arranging and assembling tours that are sold through travel agencies or directly by tour operators. The tours may include any or all of the following:  • transportation  • accommodation  • food  • visits to museums, historical or cultural sites, theatrical, musical or sporting events 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399322
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				79.9 
			</td>
		

			<td valign='top'>
				79 
			</td>
		

			<td valign='top'>
				Other reservation service and related activities 
			</td>
		

			<td valign='top'>
				799 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399323
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				79.90 
			</td>
		

			<td valign='top'>
				79.9 
			</td>
		

			<td valign='top'>
				Other reservation service and related activities 
			</td>
		

			<td valign='top'>
				7990 
			</td>
		

			<td valign='top'>
				This class includes:- other travel-related reservation services:  • reservations for transportation, hotels, restaurants, car rentals, entertainment and sport etc.- time-share exchange services- ticket sales activities for theatrical, sports and other amusement and entertainment events- visitor assistance services:  • provision of travel information to visitors  • activities of tourist guides- tourism promotion activities 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- the activities of travel agencies and tour operators, see 79.11, 79.12- organisation and management of events such as meetings, conventions and conferences, see 82.30 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399324
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				80 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				Security and investigation activities 
			</td>
		

			<td valign='top'>
				80 
			</td>
		

			<td valign='top'>
				This division includes security-related services such as: investigation and detective services; guard and patrol services; picking up and delivering money, receipts, or other valuable items with personnel and equipment to protect such properties while in transit; operation of electronic security alarm systems, such as burglar and fire alarms, where the activity focuses on remote monitoring these systems, but often involves also sale, installation and repair services. If the latter components are provided separate, they are excluded from this division and classified in retail sale, construction etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399325
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				80.1 
			</td>
		

			<td valign='top'>
				80 
			</td>
		

			<td valign='top'>
				Private security activities 
			</td>
		

			<td valign='top'>
				801 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399326
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				80.10 
			</td>
		

			<td valign='top'>
				80.1 
			</td>
		

			<td valign='top'>
				Private security activities 
			</td>
		

			<td valign='top'>
				8010 
			</td>
		

			<td valign='top'>
				This class includes the provision of one or more of the following: guard and patrol services, picking up and delivering money, receipts, or other valuable items with personnel and equipment to protect such properties while in transit.This class includes:- armoured car services- bodyguard services- polygraph services- fingerprinting services- security guard services- security shredding of information on any media 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Support services to cash collection and deposit services 
			</td>
		

			<td valign='top'>
				This class excludes:- public order and safety activities, see 84.24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399327
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				80.2 
			</td>
		

			<td valign='top'>
				80 
			</td>
		

			<td valign='top'>
				Security systems service activities 
			</td>
		

			<td valign='top'>
				802 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399328
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				80.20 
			</td>
		

			<td valign='top'>
				80.2 
			</td>
		

			<td valign='top'>
				Security systems service activities 
			</td>
		

			<td valign='top'>
				8020 
			</td>
		

			<td valign='top'>
				This class includes:- monitoring or remote monitoring of electronic security alarm systems, such as burglar and fire alarms, including their installation and maintenance- installing, repairing, rebuilding, and adjusting mechanical or electronic locking devices, safes and security vaults in connection with later monitoring and remote monitoringThe units carrying out these activities may also engage in selling such security systems, mechanical or electronic locking devices, safes and security vaults. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- installation of security systems, such as burglar and fire alarms, without later monitoring, see 43.21- retail sale of electrical security alarm systems, mechanical or electronic locking devices, safes and security vaults in specialised stores, without monitoring, installation or maintenance services, see 47.59- security consultants, see 74.90- public order and safety activities, see 84.24- providing key duplication services, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399329
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				80.3 
			</td>
		

			<td valign='top'>
				80 
			</td>
		

			<td valign='top'>
				Investigation activities 
			</td>
		

			<td valign='top'>
				803 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399330
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				80.30 
			</td>
		

			<td valign='top'>
				80.3 
			</td>
		

			<td valign='top'>
				Investigation activities 
			</td>
		

			<td valign='top'>
				8030 
			</td>
		

			<td valign='top'>
				This class includes:- investigation and detective service activities- activities of all private investigators, independent of the type of client or purpose of investigation 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399331
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				81 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				Services to buildings and landscape activities 
			</td>
		

			<td valign='top'>
				81 
			</td>
		

			<td valign='top'>
				This division includes the provision of a number of general support services, such as the provision of a combination of support services within a client's facilities, the interior and exterior cleaning of buildings of all types, cleaning of industrial machinery, cleaning of trains, buses, planes, etc., cleaning of the inside of road and sea tankers, disinfecting and exterminating activities for buildings, ships, trains, etc., bottle cleaning, street sweeping, snow and ice removal, provision of landscape care and maintenance services and provision of these services along with the design of landscape plans and/or the construction (i.e. installation) of walkways, retaining walls, decks, fences, ponds, and similar structures. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399332
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				81.1 
			</td>
		

			<td valign='top'>
				81 
			</td>
		

			<td valign='top'>
				Combined facilities support activities 
			</td>
		

			<td valign='top'>
				811 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399333
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				81.10 
			</td>
		

			<td valign='top'>
				81.1 
			</td>
		

			<td valign='top'>
				Combined facilities support activities 
			</td>
		

			<td valign='top'>
				8110 
			</td>
		

			<td valign='top'>
				This class includes the provision of a combination of support services within a client's facilities. These services include general interior cleaning, maintenance, trash disposal, guard and security, mail routing, reception, laundry and related services to support operations within facilities. These support activities are performed by operating staff, which is not involved with or responsible for the core business or activities of the client. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- provision of only one of the support services (e.g. general interior cleaning services) or addressing only a single function (e.g. heating), see the appropriate class according to the service provided- provision of management and operating staff for the complete operation of a client's establishment, such as a hotel, restaurant, mine, or hospital, see the class of the unit operated- provision of on site management and operation of a client's computer systems and/or data processing facilities, see 62.03- operation of correctional facilities on a contract or fee basis, see 84.23 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399334
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				81.2 
			</td>
		

			<td valign='top'>
				81 
			</td>
		

			<td valign='top'>
				Cleaning activities 
			</td>
		

			<td valign='top'>
				812 
			</td>
		

			<td valign='top'>
				This group includes the activities of general interior cleaning of all types of buildings, exterior cleaning of buildings, specialised cleaning activities for buildings or other specialised cleaning activities, cleaning of industrial machinery, cleaning of the inside of road and sea tankers, disinfecting and extermination activities for buildings and industrial machinery, bottle cleaning, street sweeping, snow and ice removal. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- agricultural pest control, see 01.61- cleaning of new buildings immediately after construction, 43.39- steam-cleaning, sand blasting and similar activities for building exteriors, see 43.99- carpet and rug shampooing, drapery and curtain cleaning, see 96.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399335
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				81.21 
			</td>
		

			<td valign='top'>
				81.2 
			</td>
		

			<td valign='top'>
				General cleaning of buildings 
			</td>
		

			<td valign='top'>
				8121 
			</td>
		

			<td valign='top'>
				This class includes:- general (non-specialized) cleaning of all types of buildings, such as:  • offices   • houses or apartments  • factories   • shops   • institutions- general (non-specialized) cleaning of other business and professional premises and multiunit residential buildingsThese activities are mostly interior cleaning although they may include the cleaning of associated exterior areas such as windows or passageways. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- specialised cleaning activities, such as window cleaning, chimney cleaning, cleaning of fireplaces, stoves, furnaces, incinerators, boilers, ventilation ducts, exhaust units, see 81.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399336
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				81.22 
			</td>
		

			<td valign='top'>
				81.2 
			</td>
		

			<td valign='top'>
				Other building and industrial cleaning activities 
			</td>
		

			<td valign='top'>
				8129 
			</td>
		

			<td valign='top'>
				This class includes:- exterior cleaning of buildings of all types, including offices, factories, shops, institutions and other business and professional premises and multiunit residential buildings- specialised cleaning activities for buildings such as window cleaning, chimney cleaning and cleaning of fireplaces, stoves, furnaces, incinerators, boilers, ventilation ducts and exhaust units- cleaning of industrial machinery- other building and industrial cleaning activities, n.e.c. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Cleaning of water supply pipelines 
			</td>
		

			<td valign='top'>
				This class excludes:- steam cleaning and blasting and similar activities for building exteriors, see 43.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399337
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				81.29 
			</td>
		

			<td valign='top'>
				81.2 
			</td>
		

			<td valign='top'>
				Other cleaning activities 
			</td>
		

			<td valign='top'>
				8129 
			</td>
		

			<td valign='top'>
				This class includes:- swimming pool cleaning and maintenance activities- cleaning of trains, buses, planes, etc.- cleaning of the inside of road and sea tankers - disinfecting and exterminating activities- bottle cleaning- street sweeping and snow and ice removal- other cleaning activities, n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- agriculture pest control, see 01.61- automobile cleaning, car wash, see 45.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399338
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				81.3 
			</td>
		

			<td valign='top'>
				81 
			</td>
		

			<td valign='top'>
				Landscape service activities 
			</td>
		

			<td valign='top'>
				813 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399339
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				81.30 
			</td>
		

			<td valign='top'>
				81.3 
			</td>
		

			<td valign='top'>
				Landscape service activities 
			</td>
		

			<td valign='top'>
				8130 
			</td>
		

			<td valign='top'>
				This class includes:- planting, care and maintenance of:  • parks and gardens for:    • private and public housing    • public and semi-public buildings (schools, hospitals, administrative buildings, church buildings etc.)    • municipal grounds (parks, green areas, cemeteries etc.)    • highway greenery (roads, train lines and tramlines, waterways, ports)    • industrial and commercial buildings- greenery for:  • buildings (roof gardens, façade greenery, indoor gardens etc.)  • sports grounds (football fields, golf courses etc.), play grounds, lawns for sunbathing and other recreational parks  • stationary and flowing water (basins, alternating wet areas, ponds, swimming pools, ditches, watercourses, plant sewage systems)- plants for protection against noise, wind, erosion, visibility and dazzling 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- commercial production and planting for commercial production of plants, trees, see divisions 01, 02- tree nurseries and forest tree nurseries, see 01.30, 02.10- keeping the land in good environmental condition for agricultural use, see 01.61- construction activities for landscaping purposes, see section F- landscape design and architecture activities, see 71.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399340
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				82 
			</td>
		

			<td valign='top'>
				N 
			</td>
		

			<td valign='top'>
				Office administrative, office support and other business support activities 
			</td>
		

			<td valign='top'>
				82 
			</td>
		

			<td valign='top'>
				This division includes the provision of a range of day-to-day office administrative services, as well as ongoing routine business support functions for others, on a contract or fee basis. 
			</td>
		

			<td valign='top'>
				This division also includes all support service activities typically provided to businesses not elsewhere classified. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				Units classified in this division do not provide operating staff to carry out the complete operations of a business. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399341
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				82.1 
			</td>
		

			<td valign='top'>
				82 
			</td>
		

			<td valign='top'>
				Office administrative and support activities 
			</td>
		

			<td valign='top'>
				821 
			</td>
		

			<td valign='top'>
				This group includes the provision of a range of day-to-day office administrative services, such as financial planning, billing and record keeping, personnel and physical distribution and logistics for others on a contract or fee basis. 
			</td>
		

			<td valign='top'>
				This group also includes support activities for others on a contract or fee basis, that are ongoing routine business support functions that businesses and organisations traditionally do for themselves. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				Units classified in this group do not provide operating staff to carry out the complete operations of a business. Units engaged in one particular aspect of these activities are classified according to that particular activity. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399342
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.11 
			</td>
		

			<td valign='top'>
				82.1 
			</td>
		

			<td valign='top'>
				Combined office administrative service activities 
			</td>
		

			<td valign='top'>
				8211 
			</td>
		

			<td valign='top'>
				This class includes the provision of a combination of day-to-day office administrative services, such as reception, financial planning, billing and record keeping, personnel and mail services etc. for others on a contract or fee basis. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- provision of only one particular aspect of these activities, see class according to that particular activity- provision of the operating staff without supervision, see 78 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399343
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.19 
			</td>
		

			<td valign='top'>
				82.1 
			</td>
		

			<td valign='top'>
				Photocopying, document preparation and other specialised office support activities 
			</td>
		

			<td valign='top'>
				8219 
			</td>
		

			<td valign='top'>
				This class includes a variety of copying, document preparation and specialised office support activities. The document copying/printing activities included here cover only short-run type printing activities.This class includes: - document preparation- document editing or proofreading - typing and word processing - secretarial support services- transcription of documents, and other secretarial services- letter or resume writing - provision of mailbox rental and other postal and mailing services, such as pre-sorting, addressing, etc. - photocopying - duplicating- blueprinting- other document copying services (without also providing printing services, such as offset printing, quick printing, digital printing, pre-press services) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Document and image scanning activities- Formatting of eBooks- Document preparation and pre-archiving 
			</td>
		

			<td valign='top'>
				This class excludes:- printing of documents (offset printing, quick printing etc.), see 18.12- pre-press services, see 18.13- developing and organising mail advertising campaigns, see 73.11- specialised stenotype services such as court reporting, see 82.99- public stenography services, see 82.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399344
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				82.2 
			</td>
		

			<td valign='top'>
				82 
			</td>
		

			<td valign='top'>
				Activities of call centres 
			</td>
		

			<td valign='top'>
				822 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399345
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.20 
			</td>
		

			<td valign='top'>
				82.2 
			</td>
		

			<td valign='top'>
				Activities of call centres 
			</td>
		

			<td valign='top'>
				8220 
			</td>
		

			<td valign='top'>
				This class includes the activities of: - inbound call centres, answering calls from clients by using human operators, automatic call distribution, computer telephone integration, interactive voice response systems or similar methods to receive orders, provide product information, deal with customer requests for assistance or address customer complaints - outbound call centres using similar methods to sell or market goods or services to potential customers, undertake market research or public opinion polling and similar activities for clients 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399346
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				82.3 
			</td>
		

			<td valign='top'>
				82 
			</td>
		

			<td valign='top'>
				Organisation of conventions and trade shows 
			</td>
		

			<td valign='top'>
				823 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399347
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.30 
			</td>
		

			<td valign='top'>
				82.3 
			</td>
		

			<td valign='top'>
				Organisation of conventions and trade shows 
			</td>
		

			<td valign='top'>
				8230 
			</td>
		

			<td valign='top'>
				This class includes the organisation, promotion and/or management of events, such as business and trade shows, conventions, conferences and meetings, whether or not including the management and provision of the staff to operate the facilities in which these events take place. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399348
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				82.9 
			</td>
		

			<td valign='top'>
				82 
			</td>
		

			<td valign='top'>
				Business support service activities n.e.c. 
			</td>
		

			<td valign='top'>
				829 
			</td>
		

			<td valign='top'>
				This group includes the activities of collection agencies, credit bureaus and all support activities typically provided to businesses not elsewhere classified. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399349
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.91 
			</td>
		

			<td valign='top'>
				82.9 
			</td>
		

			<td valign='top'>
				Activities of collection agencies and credit bureaus 
			</td>
		

			<td valign='top'>
				8291 
			</td>
		

			<td valign='top'>
				This class includes:- collection of payments for claims and remittance of payments collected to the clients, such as bill or debt collection services- compiling of information, such as credit and employment histories on individuals and credit histories on businesses and providing the information to financial institutions, retailers and others who have a need to evaluate the creditworthiness of these persons and businesses 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Credit score information compilation activities 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399350
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.92 
			</td>
		

			<td valign='top'>
				82.9 
			</td>
		

			<td valign='top'>
				Packaging activities 
			</td>
		

			<td valign='top'>
				8292 
			</td>
		

			<td valign='top'>
				This class includes:- packaging activities on a fee or contract basis, whether or not these involve an automated process:  • bottling of liquids, including beverages and food  • packaging of solids (blister packaging, foil-covered etc.)  • security packaging of pharmaceutical preparations  • labelling, stamping and imprinting  • parcel-packing and gift-wrapping 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Potato powder/flakes packaging under inert atmosphere- Sterilisation by ionisation 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of soft drinks and production of mineral water, see 11.07- packaging activities incidental to transport, see 52.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399351
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				82.99 
			</td>
		

			<td valign='top'>
				82.9 
			</td>
		

			<td valign='top'>
				Other business support service activities n.e.c. 
			</td>
		

			<td valign='top'>
				8299 
			</td>
		

			<td valign='top'>
				This class includes:- providing verbatim reporting and stenotype recording of live legal proceedings and transcribing subsequent recorded materials, such as:  • court reporting or stenotype recording services  • public stenography services- real-time (i.e. simultaneous) closed captioning of live television performances of meetings, conferences - address bar coding services- bar code imprinting services- fundraising organisation services on a contract or fee basis- repossession services- parking meter coin collection services- activities of independent auctioneers - administration of loyalty programmes- other support activities typically provided to businesses not elsewhere classified 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Restaurant voucher handling service activities- Providing assistance in motor vehicle registration- Relocation service activities- Leisure activities gift package administration- Voucher issuing and handling activities- Charity crowd funding- Credit intermediation 
			</td>
		

			<td valign='top'>
				This class excludes:- provision of document transcription services, see 82.19- providing film or tape captioning or subtitling services, see 59.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399352
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				O 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				PUBLIC ADMINISTRATION AND DEFENCE; COMPULSORY SOCIAL SECURITY 
			</td>
		

			<td valign='top'>
				O 
			</td>
		

			<td valign='top'>
				This section includes activities of a governmental nature, normally carried out by the public administration. This includes the enactment and judicial interpretation of laws and their pursuant regulation, as well as the administration of programmes based on them, legislative activities, taxation, national defence, public order and safety, immigration services, foreign affairs and the administration of government programmes. The legal or institutional status is not, in itself, the determining factor for an activity to belong in this section, rather than the activity being of a nature specified in the previous paragraph. This means that activities classified elsewhere in NACE do not fall under this section, even if carried out by public entities. For example, administration of the school system (i.e. regulations, checks, curricula) falls under this section, but teaching itself does not (see section P), and a prison or military hospital is classified to health (see section Q). Similarly, some activities described in this section may be carried out by non-government units. 
			</td>
		

			<td valign='top'>
				This section also includes compulsory social security activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399353
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				84 
			</td>
		

			<td valign='top'>
				O 
			</td>
		

			<td valign='top'>
				Public administration and defence; compulsory social security 
			</td>
		

			<td valign='top'>
				84 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399354
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				84.1 
			</td>
		

			<td valign='top'>
				84 
			</td>
		

			<td valign='top'>
				Administration of the State and the economic and social policy of the community 
			</td>
		

			<td valign='top'>
				841 
			</td>
		

			<td valign='top'>
				This group includes general administration (e.g. executive, legislative, financial administration etc. at all levels of government) and supervision in the field of social and economic life. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399355
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.11 
			</td>
		

			<td valign='top'>
				84.1 
			</td>
		

			<td valign='top'>
				General public administration activities 
			</td>
		

			<td valign='top'>
				8411 
			</td>
		

			<td valign='top'>
				This class includes:- executive and legislative administration of central, regional and local bodies- administration and supervision of fiscal affairs:  • operation of taxation schemes  • duty/tax collection on goods and tax violation investigation  • customs administration- budget implementation and management of public funds and public debt:  • raising and receiving of money and control of their disbursement- administration of overall (civil) research and development policy and associated funds- administration and operation of overall economic and social planning and statistical services at the various levels of government 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operation of government owned or occupied buildings, see 68.2, 68.3- administration of research and development policies intended to increase personal well-being and of associated funds, see 84.12- administration of research and development policies intended to improve economic performance and competitiveness, see 84.13- administration of defence-related research and development policies and of associated funds, see 84.22- operation of government archives, see 91.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399356
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.12 
			</td>
		

			<td valign='top'>
				84.1 
			</td>
		

			<td valign='top'>
				Regulation of the activities of providing health care, education, cultural services and other social services, excluding social security 
			</td>
		

			<td valign='top'>
				8412 
			</td>
		

			<td valign='top'>
				This class includes:- public administration of programmes aimed to increase personal well-being:  • health  • education  • culture  • sport  • recreation  • environment  • housing  • social services- public administration of research and development policies and associated funds for these areas 
			</td>
		

			<td valign='top'>
				This class also includes: - sponsoring of recreational and cultural activities- distribution of public grants to artists- administration of potable water supply programmes- administration of waste collection and disposal operations- administration of environmental protection programmes- administration of housing programmes 
			</td>
		

			<td valign='top'>
				- Public health institutes 
			</td>
		

			<td valign='top'>
				This class excludes: - sewage, refuse disposal and remediation activities, see divisions 37, 38, 39- compulsory social security activities, see 84.30- education activities, see section P- human health-related activities, see division 86- museums and other cultural institutions, see division 91- activities of government operated libraries and archives, see 91.01- sporting or other recreational activities, see division 93 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399357
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.13 
			</td>
		

			<td valign='top'>
				84.1 
			</td>
		

			<td valign='top'>
				Regulation of and contribution to more efficient operation of businesses 
			</td>
		

			<td valign='top'>
				8413 
			</td>
		

			<td valign='top'>
				This class includes:- public administration and regulation, including subsidy allocation, for different economic sectors:   • agriculture  • land use  • energy and mining resources  • infrastructure  • transport  • communication  • hotels and tourism  • wholesale and retail trade- administration of research and development policies and associated funds to improve economic performance- administration of general labour affairs- implementation of regional development policy measures, e.g. to reduce unemployment 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- research and experimental development activities, see division 72 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399358
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				84.2 
			</td>
		

			<td valign='top'>
				84 
			</td>
		

			<td valign='top'>
				Provision of services to the community as a whole 
			</td>
		

			<td valign='top'>
				842 
			</td>
		

			<td valign='top'>
				This group includes foreign affairs, defence and public order and safety activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399359
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.21 
			</td>
		

			<td valign='top'>
				84.2 
			</td>
		

			<td valign='top'>
				Foreign affairs 
			</td>
		

			<td valign='top'>
				8421 
			</td>
		

			<td valign='top'>
				This class includes:- administration and operation of the ministry of foreign affairs and diplomatic and consular missions stationed abroad or at offices of international organisations- administration, operation and support for information and cultural services intended for distribution beyond national boundaries- aid to foreign countries, whether or not routed through international organisations- provision of military aid to foreign countries- management of foreign trade, international financial and foreign technical affairs 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- international disaster or conflict refugee services, see 88.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399360
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.22 
			</td>
		

			<td valign='top'>
				84.2 
			</td>
		

			<td valign='top'>
				Defence activities 
			</td>
		

			<td valign='top'>
				8422 
			</td>
		

			<td valign='top'>
				This class includes:- administration, supervision and operation of military defence affairs and land, sea, air and space defence forces such as:  • combat forces of army, navy and air force  • engineering, transport, communications, intelligence, material, personnel and other non-combat forces and commands  • reserve and auxiliary forces of the defence establishment  • military logistics (provision of equipment, structures, supplies etc.)  • health activities for military personnel in the field- administration, operation and support of civil defence forces- support for the working out of contingency plans and the carrying out of exercises in which civilian institutions and populations are involved- administration of defence-related research and development policies and related funds 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- research and experimental development activities, see division 72- provision of military aid to foreign countries, see 84.21- activities of military tribunals, see 84.23 - provision of supplies for domestic emergency use in case of peacetime disasters, see 84.24 - educational activities of military schools, colleges and academies, see 85.4- activities of military hospitals, see 86.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399361
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.23 
			</td>
		

			<td valign='top'>
				84.2 
			</td>
		

			<td valign='top'>
				Justice and judicial activities 
			</td>
		

			<td valign='top'>
				8423 
			</td>
		

			<td valign='top'>
				This class includes:- administration and operation of administrative civil and criminal law courts, military tribunals and the judicial system, including legal representation and advice on behalf of the government or when provided by the government in cash or services- rendering of judgements and interpretations of the law- arbitration of civil actions- prison administration and provision of correctional services, including rehabilitation services, regardless of whether their administration and operation is done by government units or by private units on a contract or fee basis 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- advice and representation in civil, criminal and other cases, see 69.10- activities of prison schools, see division 85- activities of prison hospitals, see 86.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399362
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.24 
			</td>
		

			<td valign='top'>
				84.2 
			</td>
		

			<td valign='top'>
				Public order and safety activities 
			</td>
		

			<td valign='top'>
				8423 
			</td>
		

			<td valign='top'>
				This class includes:- administration and operation of regular and auxiliary police forces supported by public authorities and of port, border, coastguards and other special police forces, including traffic regulation, alien registration, maintenance of arrest records- provision of supplies for domestic emergency use in case of peacetime disasters 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operation of police laboratories, see 71.20- administration and operation of military armed forces, see 84.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399363
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.25 
			</td>
		

			<td valign='top'>
				84.2 
			</td>
		

			<td valign='top'>
				Fire service activities 
			</td>
		

			<td valign='top'>
				8423 
			</td>
		

			<td valign='top'>
				This class includes:- fire fighting and fire prevention:  • administration and operation of regular and auxiliary fire brigades in fire prevention, fire fighting, rescue of persons and animals, assistance in civic disasters, floods, road accidents etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- forestry fire protection and fire fighting services, see 02.40- oil and gas field fire fighting, see 09.10- fire fighting and fire prevention services at airports provided by non-specialised units, see 52.23 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399364
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				84.3 
			</td>
		

			<td valign='top'>
				84 
			</td>
		

			<td valign='top'>
				Compulsory social security activities 
			</td>
		

			<td valign='top'>
				843 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399365
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				84.30 
			</td>
		

			<td valign='top'>
				84.3 
			</td>
		

			<td valign='top'>
				Compulsory social security activities 
			</td>
		

			<td valign='top'>
				8430 
			</td>
		

			<td valign='top'>
				This class includes:- funding and administration of government-provided social security programmes:  • sickness, work-accident and unemployment insurance  • retirement pensions  • programmes covering losses of income due to maternity, temporary disablement, widowhood etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- non-compulsory social security, see 65.30- provision of welfare services and social work (without accommodation), see 88.10, 88.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399366
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				P 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				EDUCATION 
			</td>
		

			<td valign='top'>
				P 
			</td>
		

			<td valign='top'>
				This section includes education at any level or for any profession. The instructions may be oral or written and may be provided by radio, television, Internet or via correspondence. It includes education by the different institutions in the regular school system at its different levels as well as adult education, literacy programmes etc. Also included are military schools and academies, prison schools etc. at their respective levels. The section includes public as well as private education.For each level of initial education, the classes include special education for physically or mentally handicapped pupils. The breakdown of the categories in this section is based on the level of education offered as defined by the levels of ISCED 1997. The activities of educational institutions providing courses on ISCED level 0 are classified in 85.10, on ISCED level 1 in 85.20, on ISCED levels 2-3 in group 85.3, on ISCED level 4 in 85.41 and on ISCED level 5-6 in 85.42. 
			</td>
		

			<td valign='top'>
				This section also includes instruction primarily concerned with sport and recreational activities such as tennis or golf and education support activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399367
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				P 
			</td>
		

			<td valign='top'>
				Education 
			</td>
		

			<td valign='top'>
				85 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399368
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				85.1 
			</td>
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				Pre-primary education 
			</td>
		

			<td valign='top'>
				851 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399369
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.10 
			</td>
		

			<td valign='top'>
				85.1 
			</td>
		

			<td valign='top'>
				Pre-primary education 
			</td>
		

			<td valign='top'>
				8510 
			</td>
		

			<td valign='top'>
				This class includes:- pre-primary education (education preceding the first level). Pre-primary education is defined as the initial stage of organised instruction designed primarily to introduce very young children to a school-type environment, that is, to provide a bridge between the home and a school-based atmosphere. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- child day-care activities, see 88.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399370
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				85.2 
			</td>
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				Primary education 
			</td>
		

			<td valign='top'>
				851 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399371
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.20 
			</td>
		

			<td valign='top'>
				85.2 
			</td>
		

			<td valign='top'>
				Primary education 
			</td>
		

			<td valign='top'>
				8510 
			</td>
		

			<td valign='top'>
				This class includes primary education: the furnishing of academic courses and associated course work that give students a sound basic education in reading, writing and mathematics and an elementary understanding of other subjects such as history, geography, natural science, social science, art and music. Such education is generally provided for children, however the provision of literacy programmes within or outside the school system, which are similar in content to programmes in primary education but are intended for those considered too old to enter elementary schools, is also included (i.e. adult literacy programmes). 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- adult education as defined in 85.5- child day-care activities, including day nurseries for pupils, see 88.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399372
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				85.3 
			</td>
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				Secondary education 
			</td>
		

			<td valign='top'>
				852 
			</td>
		

			<td valign='top'>
				This group includes the provision of general secondary and technical and vocational secondary education. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- adult education as defined in 85.5 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399373
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.31 
			</td>
		

			<td valign='top'>
				85.3 
			</td>
		

			<td valign='top'>
				General secondary education 
			</td>
		

			<td valign='top'>
				8521 
			</td>
		

			<td valign='top'>
				This class includes provision of the type of education that lays the foundation for lifelong learning and human development and is capable of furthering education opportunities. Such units provide programmes that are usually on a more subject-oriented pattern using more specialised teachers, and more often employ several teachers conducting classes in their field of specialisation. Subject specialisation at this level often begins to have some influence even on the educational experience of those pursuing a general programme. Such programmes are designated to qualify students either for technical and vocational education or for entrance to higher education without any special subject prerequisite.This class includes:- lower general secondary education corresponding more or less to the period of compulsory school attendance- upper general secondary education giving, in principle, access to higher education 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399374
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.32 
			</td>
		

			<td valign='top'>
				85.3 
			</td>
		

			<td valign='top'>
				Technical and vocational secondary education 
			</td>
		

			<td valign='top'>
				8522 
			</td>
		

			<td valign='top'>
				This class includes provision of education typically emphasising subject-matter specialisation and instruction in both theoretical background and practical skills generally associated with present or prospective employment. The aim of a programme can vary from preparation for a general field of employment to a very specific job.This class includes:- technical and vocational education below the level of higher education as defined in 85.4 
			</td>
		

			<td valign='top'>
				This class also includes:- tourist guide instruction- instruction for chefs, hoteliers and restaurateurs- cosmetology and barber schools- computer repair training- driving schools for occupational drivers e.g. of trucks, buses, coaches, schools for professional pilots 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- technical and vocational higher education, see 85.4- performing art instruction for recreation, hobby and self-development purposes, see 85.52- automobile driving schools not intended for occupational drivers, see 85.53- job training forming part of social work activities without accommodation, see 88.10, 88.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399375
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				85.4 
			</td>
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				Higher education 
			</td>
		

			<td valign='top'>
				853 
			</td>
		

			<td valign='top'>
				This group includes the furnishing of post-secondary non-tertiary and academic courses and granting of degrees at baccalaureate, graduate or post-graduate level. The requirement for admission is a diploma at least at upper secondary education level. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes:- adult education as defined in 85.5 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399376
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.41 
			</td>
		

			<td valign='top'>
				85.4 
			</td>
		

			<td valign='top'>
				Post-secondary non-tertiary education 
			</td>
		

			<td valign='top'>
				8530 
			</td>
		

			<td valign='top'>
				This class includes provision of post-secondary education, which cannot be considered tertiary education. For example provision of supplementary post-secondary education to prepare for tertiary education or post-secondary non-tertiary vocational. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399377
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.42 
			</td>
		

			<td valign='top'>
				85.4 
			</td>
		

			<td valign='top'>
				Tertiary education 
			</td>
		

			<td valign='top'>
				8530 
			</td>
		

			<td valign='top'>
				This class includes:- first, second and third stages of tertiary education 
			</td>
		

			<td valign='top'>
				This class also includes:- performing arts schools providing tertiary education 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399378
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				85.5 
			</td>
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				Other education 
			</td>
		

			<td valign='top'>
				854 
			</td>
		

			<td valign='top'>
				This group includes general continuing education and continuing vocational education and training for any profession, hobby or self-development purposes.It includes camps and schools offering instruction in athletic activities to groups or individuals, foreign language instruction, instruction in the arts, drama or music or other instruction or specialised training, not comparable to the education in groups 85.1 - 85.4. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				It excludes educational activities as outlined in groups 85.1-85.4, i.e. pre-primary education, primary education, secondary education or higher education. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399379
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.51 
			</td>
		

			<td valign='top'>
				85.5 
			</td>
		

			<td valign='top'>
				Sports and recreation education 
			</td>
		

			<td valign='top'>
				8541 
			</td>
		

			<td valign='top'>
				This class includes the provision of instruction in athletic activities to groups of individuals, such as by camps and schools. Overnight and day sports instruction camps are also included. It does not include academic schools, colleges and universities. Instruction may be provided in diverse settings, such as the unit's or client's training facilities, educational institutions or by other means. Instruction provided in this class is formally organised.This class includes:- sports instruction (baseball, basketball, cricket, football, etc)- camps, sports instruction- gymnastics instruction- riding instruction, academies or schools- swimming instruction- professional sports instructors, teachers, coaches- martial arts instruction- card game instruction (such as bridge)- yoga instruction 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- cultural education, see 85.52 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399380
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.52 
			</td>
		

			<td valign='top'>
				85.5 
			</td>
		

			<td valign='top'>
				Cultural education 
			</td>
		

			<td valign='top'>
				8542 
			</td>
		

			<td valign='top'>
				This class includes provision of instruction in the arts, drama and music. Units giving this type of instructions might be named &quot;schools”, &quot;studios”, &quot;classes” etc. They provide formally organised instruction, mainly for hobby, recreational or self-development purposes, but such instruction does not lead to a professional diploma, baccalaureate or graduate degree.This class includes:- piano teachers and other music instruction- art instruction- dance instruction and dance studios- drama schools (except academic)- fine arts schools (except academic)- performing arts schools (except academic)- photography schools (except commercial) 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- foreign language instruction, see 85.59 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399381
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.53 
			</td>
		

			<td valign='top'>
				85.5 
			</td>
		

			<td valign='top'>
				Driving school activities 
			</td>
		

			<td valign='top'>
				8549 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- flying, sailing, shipping schools not issuing commercial certificates and permits 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- driving schools for occupational drivers, see 85.32 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399382
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.59 
			</td>
		

			<td valign='top'>
				85.5 
			</td>
		

			<td valign='top'>
				Other education n.e.c. 
			</td>
		

			<td valign='top'>
				8549 
			</td>
		

			<td valign='top'>
				This class includes:- education that is not definable by level- academic tutoring - learning centres offering remedial courses- professional examination review courses- language instruction and conversational skills instruction- computer training- religious instruction 
			</td>
		

			<td valign='top'>
				This class also includes:- lifeguard training- survival training- public speaking training- speed reading instruction 
			</td>
		

			<td valign='top'>
				- Employee training 
			</td>
		

			<td valign='top'>
				This class excludes:- adult literacy programmes see 85.20- general secondary education, see 85.31- technical and vocational secondary education, see 85.32- higher education, see 85.4 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399383
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				85.6 
			</td>
		

			<td valign='top'>
				85 
			</td>
		

			<td valign='top'>
				Educational support activities 
			</td>
		

			<td valign='top'>
				855 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399384
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				85.60 
			</td>
		

			<td valign='top'>
				85.6 
			</td>
		

			<td valign='top'>
				Educational support activities 
			</td>
		

			<td valign='top'>
				8550 
			</td>
		

			<td valign='top'>
				This class includes:- provision of non-instructional activities that support educational processes or systems:  • educational consulting  • educational guidance counselling activities  • educational testing evaluation activities  • educational testing activities  • organisation of student exchange programmes 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Examination and testing of pilots 
			</td>
		

			<td valign='top'>
				This class excludes:- research and experimental development on social sciences and humanities, see 72.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399385
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				Q 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				HUMAN HEALTH AND SOCIAL WORK ACTIVITIES 
			</td>
		

			<td valign='top'>
				Q 
			</td>
		

			<td valign='top'>
				This section includes the provision of health and social work activities. Activities include a wide range of activities, starting from health care provided by trained medical professionals in hospitals and other facilities, over residential care activities that still involve a degree of health care activities to social work activities without any involvement of health care professionals. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399386
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				86 
			</td>
		

			<td valign='top'>
				Q 
			</td>
		

			<td valign='top'>
				Human health activities 
			</td>
		

			<td valign='top'>
				86 
			</td>
		

			<td valign='top'>
				This division includes activities of short- or long-term hospitals, general or specialty medical, surgical, psychiatric and substance abuse hospitals, sanatoria, preventoria, medical nursing homes, asylums, mental hospital institutions, rehabilitation centres, leprosaria and other human health institutions which have accommodation facilities and which engage in providing diagnostic and medical treatment to inpatients with any of a wide variety of medical conditions. 
			</td>
		

			<td valign='top'>
				It also includes medical consultation and treatment in the field of general and specialised medicine by general practitioners and medical specialists and surgeons. It includes dental practice activities of a general or specialised nature and orthodontic activities. Additionally, this division includes activities for human health not performed by hospitals or by practicing medical doctors but by paramedical practitioners legally recognised to treat patients. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399387
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				86.1 
			</td>
		

			<td valign='top'>
				86 
			</td>
		

			<td valign='top'>
				Hospital activities 
			</td>
		

			<td valign='top'>
				861 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399388
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				86.10 
			</td>
		

			<td valign='top'>
				86.1 
			</td>
		

			<td valign='top'>
				Hospital activities 
			</td>
		

			<td valign='top'>
				8610 
			</td>
		

			<td valign='top'>
				This class includes:- short- or long-term hospital activities, i.e. medical, diagnostic and treatment activities, of general hospitals (e.g. community and regional hospitals, hospitals of non-profit organisations, university hospitals, military-base and prison hospitals) and specialised hospitals (e.g. mental health and substance abuse hospitals, hospitals for infectious diseases, maternity hospitals, specialised sanatoriums).The activities are chiefly directed to inpatients, are carried out under the direct supervision of medical doctors and include:  • services of medical and paramedical staff  • services of laboratory and technical facilities, including radiologic and anaesthesiologic services  • emergency room services  • provision of operating room services, pharmacy services, food and other hospital services  • services of family planning centres providing medical treatment such as sterilisation and termination of pregnancy, with accommodation 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- laboratory testing and inspection of all types of materials and products, except medical, see 71.20- veterinary activities, see 75.00- health activities for military personnel in the field, see 84.22- dental practice activities of a general or specialised nature, e.g. dentistry, endodontic and pediatric dentistry; oral pathology, orthodontic activities, see 86.23- private consultants' services to inpatients, see 86.2- medical laboratory testing, see 86.90- ambulance transport activities, see 86.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399389
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				86.2 
			</td>
		

			<td valign='top'>
				86 
			</td>
		

			<td valign='top'>
				Medical and dental practice activities 
			</td>
		

			<td valign='top'>
				862 
			</td>
		

			<td valign='top'>
				This group includes medical consultation and treatment done by general medical practitioners and medical specialists, including surgeons, dentists etc.These activities can be carried out in private practice, group practices and in hospital outpatient clinics, and in clinics such as those attached to firms, schools, homes for the aged, labour organisations and fraternal organisations, as well as in patients' homes. 
			</td>
		

			<td valign='top'>
				This group also includes:- private consultants' services to inpatients 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399390
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				86.21 
			</td>
		

			<td valign='top'>
				86.2 
			</td>
		

			<td valign='top'>
				General medical practice activities 
			</td>
		

			<td valign='top'>
				8620 
			</td>
		

			<td valign='top'>
				This class includes:- medical consultation and treatment in the field of general medicine carried out by general practitioners. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- inpatient hospital activities, see 86.10- paramedical activities such as those of midwives, nurses and physiotherapists, see 86.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399391
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				86.22 
			</td>
		

			<td valign='top'>
				86.2 
			</td>
		

			<td valign='top'>
				Specialist medical practice activities 
			</td>
		

			<td valign='top'>
				8620 
			</td>
		

			<td valign='top'>
				This class includes:- medical consultation and treatment in the field of specialised medicine by medical specialists and surgeons 
			</td>
		

			<td valign='top'>
				This class also includes:- family planning centres providing medical treatment such as sterilisation and termination of pregnancy, without accommodation 
			</td>
		

			<td valign='top'>
				- Hair transplantation 
			</td>
		

			<td valign='top'>
				This class excludes:- inpatient hospital activities, see 86.10- activities of midwives, physiotherapists and other paramedical practitioners, see 86.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399392
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				86.23 
			</td>
		

			<td valign='top'>
				86.2 
			</td>
		

			<td valign='top'>
				Dental practice activities 
			</td>
		

			<td valign='top'>
				8620 
			</td>
		

			<td valign='top'>
				This class includes:- dental practice activities of a general or specialised nature, e.g. dentistry, endodontic and pediatric dentistry; oral pathology - orthodontic activities 
			</td>
		

			<td valign='top'>
				This class also includes:- dental activities in operating rooms 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- production of artificial teeth, denture and prosthetic appliances by dental laboratories, see 32.50- inpatient hospital activities, see 86.10- activities of dental paramedical personnel such as dental hygienists, see 86.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399393
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				86.9 
			</td>
		

			<td valign='top'>
				86 
			</td>
		

			<td valign='top'>
				Other human health activities 
			</td>
		

			<td valign='top'>
				869 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399394
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				86.90 
			</td>
		

			<td valign='top'>
				86.9 
			</td>
		

			<td valign='top'>
				Other human health activities 
			</td>
		

			<td valign='top'>
				8690 
			</td>
		

			<td valign='top'>
				This class includes:- activities for human health not performed by hospitals or by medical doctors or dentists:  • activities of nurses, midwives, physiotherapists or other paramedical practitioners in the field of optometry, hydrotherapy, medical massage, occupational therapy, speech therapy, chiropody, homeopathy, chiropractic, acupuncture etc.These activities may be carried out in health clinics such as those attached to firms, schools, homes for the aged, labour organisations and fraternal organisations and in residential health facilities other than hospitals, as well as in own consulting rooms, patients' homes or elsewhere. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of dental paramedical personnel such as dental therapists, school dental nurses and dental hygienists, who may work remote from, but are periodically supervised by, the dentist- activities of medical laboratories such as:  • X-ray laboratories and other diagnostic imaging centres  • blood analysis laboratories- activities of blood banks, sperm banks, transplant organ banks etc.- ambulance transport of patients by any mode of transport including airplanes. These services are often provided during a medical emergency. 
			</td>
		

			<td valign='top'>
				- Reflex therapy- Imaging scanning for medical purposes- Colon hydrotherapy- Echography- Activities of shiatsu practicians 
			</td>
		

			<td valign='top'>
				This class excludes:- production of artificial teeth, denture and prosthetic appliances by dental laboratories, see 32.50- transfer of patients, with neither equipment for lifesaving nor medical personnel, see divisions 49, 50, 51- non-medical laboratory testing, see 71.20- testing activities in the field of food hygiene, see 71.20- hospital activities, see 86.10- medical and dental practice activities, see 86.2- residential nursing care facilities, see 87.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399395
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				87 
			</td>
		

			<td valign='top'>
				Q 
			</td>
		

			<td valign='top'>
				Residential care activities 
			</td>
		

			<td valign='top'>
				87 
			</td>
		

			<td valign='top'>
				This division includes the provision of residential care combined with either nursing, supervisory or other types of care as required by the residents. Facilities are a significant part of the production process and the care provided is a mix of health and social services with the health services being largely some level of nursing services. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399396
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				87.1 
			</td>
		

			<td valign='top'>
				87 
			</td>
		

			<td valign='top'>
				Residential nursing care activities 
			</td>
		

			<td valign='top'>
				871 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399397
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				87.10 
			</td>
		

			<td valign='top'>
				87.1 
			</td>
		

			<td valign='top'>
				Residential nursing care activities 
			</td>
		

			<td valign='top'>
				8710 
			</td>
		

			<td valign='top'>
				This class includes:- activities of:  • homes for the elderly with nursing care  • convalescent homes  • rest homes with nursing care  • nursing care facilities  • nursing homes 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- in-home services provided by health care professionals, see division 86- activities of the homes for the elderly without or with minimal nursing care, see 87.30- social work activities with accommodation, such as orphanages, children's boarding homes and hostels, temporary homeless shelters, see 87.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399398
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				87.2 
			</td>
		

			<td valign='top'>
				87 
			</td>
		

			<td valign='top'>
				Residential care activities for mental retardation, mental health and substance abuse 
			</td>
		

			<td valign='top'>
				872 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399399
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				87.20 
			</td>
		

			<td valign='top'>
				87.2 
			</td>
		

			<td valign='top'>
				Residential care activities for mental retardation, mental health and substance abuse 
			</td>
		

			<td valign='top'>
				8720 
			</td>
		

			<td valign='top'>
				This class includes the provision of residential care (but not licensed hospital care) to people with mental retardation, mental illness, or substance abuse problems. Facilities provide room, board, protective supervision and counselling and some health care. This class includes:- activities of:  • facilities for alcoholism or drug addiction treatment  • psychiatric convalescent homes  • residential group homes for the emotionally disturbed  • mental retardation facilities  • mental health halfway houses 
			</td>
		

			<td valign='top'>
				This class also includes:- provision of residential care and treatment for patients with mental health and substance abuse illnesses 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- mental hospitals, see 86.10- social work activities with accommodation, such as temporary homeless shelters, see 87.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399400
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				87.3 
			</td>
		

			<td valign='top'>
				87 
			</td>
		

			<td valign='top'>
				Residential care activities for the elderly and disabled 
			</td>
		

			<td valign='top'>
				873 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399401
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				87.30 
			</td>
		

			<td valign='top'>
				87.3 
			</td>
		

			<td valign='top'>
				Residential care activities for the elderly and disabled 
			</td>
		

			<td valign='top'>
				8730 
			</td>
		

			<td valign='top'>
				This class includes the provision of residential and personal care services for the elderly and disabled who are unable to fully care for themselves and/or who do not desire to live independently. The care typically includes room, board, supervision, and assistance in daily living, such as housekeeping services. In some instances these units provide skilled nursing care for residents in separate on-site facilities.This class includes:- activities of:  • assisted-living facilities  • continuing care retirement communities  • homes for the elderly with minimal nursing care  • rest homes without nursing care 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of homes for the elderly with nursing care, see 87.10- social work activities with accommodation where medical treatment or education are not important elements, see 87.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399402
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				87.9 
			</td>
		

			<td valign='top'>
				87 
			</td>
		

			<td valign='top'>
				Other residential care activities 
			</td>
		

			<td valign='top'>
				879 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399403
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				87.90 
			</td>
		

			<td valign='top'>
				87.9 
			</td>
		

			<td valign='top'>
				Other residential care activities 
			</td>
		

			<td valign='top'>
				8790 
			</td>
		

			<td valign='top'>
				This class includes the provision of residential and personal care services for persons, except the elderly and disabled, who are unable to fully care for themselves or who do not desire to live independently.This class includes:- activities provided on a round-the-clock basis directed to provide social assistance to children and special categories of persons with some limits on ability for self-care, but where medical treatment or education are not important elements:  • orphanages  • children's boarding homes and hostels  • temporary homeless shelters  • institutions that take care of unmarried mothers and their childrenThe activities may be carried out by government offices or private organisations. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of:  • halfway group homes for persons with social or personal problems  • halfway homes for delinquents and offenders  • juvenile correction homes 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- funding and administration of compulsory social security programmes, see 84.30- activities of nursing care facilities, see 87.10- residential care activities for the elderly or disabled, see 87.30- adoption activities, see 88.99- short-term shelter activities for disaster victims, see 88.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399404
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				88 
			</td>
		

			<td valign='top'>
				Q 
			</td>
		

			<td valign='top'>
				Social work activities without accommodation 
			</td>
		

			<td valign='top'>
				88 
			</td>
		

			<td valign='top'>
				This division includes the provision of a variety of social assistance services directly to clients. The activities in this division do not include accommodation services, except on a temporary basis. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399405
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				88.1 
			</td>
		

			<td valign='top'>
				88 
			</td>
		

			<td valign='top'>
				Social work activities without accommodation for the elderly and disabled 
			</td>
		

			<td valign='top'>
				881 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399406
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				88.10 
			</td>
		

			<td valign='top'>
				88.1 
			</td>
		

			<td valign='top'>
				Social work activities without accommodation for the elderly and disabled 
			</td>
		

			<td valign='top'>
				8810 
			</td>
		

			<td valign='top'>
				This class includes:- social, counselling, welfare, referral and similar services which are aimed at the elderly and disabled in their homes or elsewhere and carried out by government offices or by private organisations, national or local self-help organisations and by specialists providing counselling services:  • visiting of the elderly and disabled  • day-care activities for the elderly or for disabled adults  • vocational rehabilitation and habilitation activities for disabled persons provided that the education component is limited 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- funding and administration of compulsory social security programmes, see 84.30- activities similar to those described in this class, but including accommodation, see 87.30- day-care activities for disabled children, see 88.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399407
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				88.9 
			</td>
		

			<td valign='top'>
				88 
			</td>
		

			<td valign='top'>
				Other social work activities without accommodation 
			</td>
		

			<td valign='top'>
				889 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399408
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				88.91 
			</td>
		

			<td valign='top'>
				88.9 
			</td>
		

			<td valign='top'>
				Child day-care activities 
			</td>
		

			<td valign='top'>
				8890 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class also includes:- activities of day nurseries for pupils, including day-care activities for disabled children 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399409
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				88.99 
			</td>
		

			<td valign='top'>
				88.9 
			</td>
		

			<td valign='top'>
				Other social work activities without accommodation n.e.c. 
			</td>
		

			<td valign='top'>
				8890 
			</td>
		

			<td valign='top'>
				This class includes:- social, counselling, welfare, refugee, referral and similar services which are delivered to individuals and families in their homes or elsewhere and carried out by government offices or by private organisations, disaster relief organisations and national or local self-help organisations and by specialists providing counselling services:  • welfare and guidance activities for children and adolescents  • adoption activities, activities for the prevention of cruelty to children and others  • household budget counselling, marriage and family guidance, credit and debt counselling services  • community and neighbourhood activities  • activities for disaster victims, refugees, immigrants etc., including temporary or extended shelter for them  • vocational rehabilitation and habilitation activities for unemployed persons provided that the education component is limited  • eligibility determination in connection with welfare aid, rent supplements or food stamps  • day facilities for the homeless and other socially weak groups  • charitable activities like fund-raising or other supporting activities aimed at social work 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Adoption organisation activities- Cultural mediation- Refugee resettlement activities 
			</td>
		

			<td valign='top'>
				This class excludes:- funding and administration of compulsory social security programmes, see 84.30- activities similar to those described in this class, but including accommodation, see 87.90 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399410
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				R 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				ARTS, ENTERTAINMENT AND RECREATION 
			</td>
		

			<td valign='top'>
				R 
			</td>
		

			<td valign='top'>
				This section includes a wide range of activities to meet varied cultural, entertainment and recreational interests of the general public, including live performances, operation of museum sites, gambling, sports and recreation activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399411
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				90 
			</td>
		

			<td valign='top'>
				R 
			</td>
		

			<td valign='top'>
				Creative, arts and entertainment activities 
			</td>
		

			<td valign='top'>
				90 
			</td>
		

			<td valign='top'>
				This division includes the operation of facilities and provision of services to meet the cultural and entertainment interests of their customers. This includes the production and promotion of, and participation in, live performances, events or exhibits intended for public viewing; the provision of artistic, creative or technical skills for the production of artistic products and live performances. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This division excludes:- the operation of museums of all kinds, botanical and zoological gardens; the preservation of historical sites; and nature reserves activities, see division 91 - gambling and betting activities, see division 92 - sports and amusement and recreation activities, see division 93.Some units that provide cultural, entertainment or recreational facilities and services are classified in other divisions, such as:- motion picture and video production and distribution, see 59.11, 59.12, 59.13- motion picture projection, see 59.14- radio and television broadcasting, see 60.1, 60.2 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399412
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				90.0 
			</td>
		

			<td valign='top'>
				90 
			</td>
		

			<td valign='top'>
				Creative, arts and entertainment activities 
			</td>
		

			<td valign='top'>
				900 
			</td>
		

			<td valign='top'>
				This group includes activities in the creative and performing arts and related activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399413
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				90.01 
			</td>
		

			<td valign='top'>
				90.0 
			</td>
		

			<td valign='top'>
				Performing arts 
			</td>
		

			<td valign='top'>
				9000 
			</td>
		

			<td valign='top'>
				This class includes:- production of live theatrical presentations, concerts and opera or dance productions and other stage productions:  • activities of groups, circuses or companies, orchestras or bands  • activities of individual artists such as actors, dancers, musicians, lecturers or speakers 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Activities of independent organists- Activities of individual/independent photo models 
			</td>
		

			<td valign='top'>
				This class excludes:- activities of personal theatrical or artistic agents or agencies, see 74.90- casting activities, see 78.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399414
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				90.02 
			</td>
		

			<td valign='top'>
				90.0 
			</td>
		

			<td valign='top'>
				Support activities to performing arts 
			</td>
		

			<td valign='top'>
				9000 
			</td>
		

			<td valign='top'>
				This class includes:- support activities to performing arts for production of live theatrical presentations, concerts and opera or dance productions and other stage productions:  • activities of directors, producers, stage-set designers and builders, scene shifters, lighting engineers etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of producers or entrepreneurs of arts live events, with or without facilities 
			</td>
		

			<td valign='top'>
				- Scenography- Activities of film directors- Organisation of cultural events, such as film festivals, musical or dance festivals 
			</td>
		

			<td valign='top'>
				This class excludes:- activities of personal theatrical or artistic agents or agencies, see 74.90- casting activities, see 78.10 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399415
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				90.03 
			</td>
		

			<td valign='top'>
				90.0 
			</td>
		

			<td valign='top'>
				Artistic creation 
			</td>
		

			<td valign='top'>
				9000 
			</td>
		

			<td valign='top'>
				This class includes:- activities of individual artists such as sculptors, painters, cartoonists, engravers, etchers etc.- activities of individual writers, for all subjects including fictional writing, technical writing etc.- activities of independent journalists- restoring of works of art such as paintings etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Interior polychrome restoration, furnace tile restoration and decorative painting- Texts and compositions writing on behalf of someone else- Activities of scriptwriters 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of statues, other than artistic originals, see 23.70- restoring of organs and other historical musical instruments, see 33.19- motion picture and video production, see 59.11, 59.12- restoring of furniture (except museum type restoration), see 95.24 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399416
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				90.04 
			</td>
		

			<td valign='top'>
				90.0 
			</td>
		

			<td valign='top'>
				Operation of arts facilities 
			</td>
		

			<td valign='top'>
				9000 
			</td>
		

			<td valign='top'>
				This class includes:- operation of concert and theatre halls and other arts facilities 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- operation of cinemas, see 59.14- activities of ticket agencies, see 79.90- operation of museums of all kinds, see 91.02 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399417
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				91 
			</td>
		

			<td valign='top'>
				R 
			</td>
		

			<td valign='top'>
				Libraries, archives, museums and other cultural activities 
			</td>
		

			<td valign='top'>
				91 
			</td>
		

			<td valign='top'>
				This division includes the activities of libraries and archives; the operation of museums of all kinds, botanical and zoological gardens; the operation of historical sites and nature reserves activities. 
			</td>
		

			<td valign='top'>
				It also includes the preservation and exhibition of objects, sites and natural wonders of historical, cultural or educational interest (e.g. world heritage sites, etc). 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This division excludes:- sports and amusement and recreation activities such as the operation of bathing beaches and recreation parks, see division 93 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399418
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				91.0 
			</td>
		

			<td valign='top'>
				91 
			</td>
		

			<td valign='top'>
				Libraries, archives, museums and other cultural activities 
			</td>
		

			<td valign='top'>
				910 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399419
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				91.01 
			</td>
		

			<td valign='top'>
				91.0 
			</td>
		

			<td valign='top'>
				Library and archives activities 
			</td>
		

			<td valign='top'>
				9101 
			</td>
		

			<td valign='top'>
				This class includes:- documentation and information activities of libraries of all kinds, reading, listening and viewing rooms, public archives providing service to the general public or to a special clientele, such as students, scientists, staff, members as well as operation of government archives:  • organisation of a collection, whether specialised or not  • cataloguing collections  • lending and storage of books, maps, periodicals, films, records, tapes, works of art etc.  • retrieval activities in order to comply with information requests etc.- stock photo and movie libraries and services 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399420
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				91.02 
			</td>
		

			<td valign='top'>
				91.0 
			</td>
		

			<td valign='top'>
				Museums activities 
			</td>
		

			<td valign='top'>
				9102 
			</td>
		

			<td valign='top'>
				This class includes:- operation of museums of all kinds:  • art museums, museums of jewellery, furniture, costumes, ceramics, silverware  • natural history, science and technological museums, historical museums, including military museums   • other specialised museums  • open-air museums 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of commercial art galleries, see 47.78- restoration of works of art and museum collection objects, see 90.03- activities of libraries and archives, see 91.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399421
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				91.03 
			</td>
		

			<td valign='top'>
				91.0 
			</td>
		

			<td valign='top'>
				Operation of historical sites and buildings and similar visitor attractions 
			</td>
		

			<td valign='top'>
				9102 
			</td>
		

			<td valign='top'>
				This class includes:- operation and preservation of historical sites and buildings 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- renovation and restoration of historical sites and buildings, see section F 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399422
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				91.04 
			</td>
		

			<td valign='top'>
				91.0 
			</td>
		

			<td valign='top'>
				Botanical and zoological gardens and nature reserves activities 
			</td>
		

			<td valign='top'>
				9103 
			</td>
		

			<td valign='top'>
				This class includes:- operation of botanical and zoological gardens, including children's zoos- operation of nature reserves, including wildlife preservation, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- landscape and gardening activities, see 81.30- operation of sport fishing and hunting preserves, see 93.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399423
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				92 
			</td>
		

			<td valign='top'>
				R 
			</td>
		

			<td valign='top'>
				Gambling and betting activities 
			</td>
		

			<td valign='top'>
				92 
			</td>
		

			<td valign='top'>
				This division includes the operation of gambling facilities such as casinos, bingo halls and video gaming terminals and the provision of gambling services, such as lotteries and off-track betting. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399424
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				92.0 
			</td>
		

			<td valign='top'>
				92 
			</td>
		

			<td valign='top'>
				Gambling and betting activities 
			</td>
		

			<td valign='top'>
				920 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399425
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				92.00 
			</td>
		

			<td valign='top'>
				92.0 
			</td>
		

			<td valign='top'>
				Gambling and betting activities 
			</td>
		

			<td valign='top'>
				9200 
			</td>
		

			<td valign='top'>
				This class includes gambling and betting activities such as:- sale of lottery tickets- operation (exploitation) of coin-operated gambling machines- operation of virtual gambling web sites- bookmaking and other betting operations- off-track betting- operation of casinos, including &quot;floating casinos” 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Quiz app subscription with prizes 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399426
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				93 
			</td>
		

			<td valign='top'>
				R 
			</td>
		

			<td valign='top'>
				Sports activities and amusement and recreation activities 
			</td>
		

			<td valign='top'>
				93 
			</td>
		

			<td valign='top'>
				This division includes the provision of recreational, amusement and sports activities (except museums activities, preservation of historical sites, botanical and zoological gardens and nature reserves activities; and gambling and betting activities). 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				Excluded from this division are dramatic arts, music and other arts and entertainment such as the production of live theatrical presentations, concerts and opera or dance productions and other stage productions, see division 90. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399427
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				93.1 
			</td>
		

			<td valign='top'>
				93 
			</td>
		

			<td valign='top'>
				Sports activities 
			</td>
		

			<td valign='top'>
				931 
			</td>
		

			<td valign='top'>
				This group includes the operation of sports facilities; activities of sports teams or clubs primarily participating in live sports events before a paying audience; independent athletes engaged in participating in live sporting or racing events before a paying audience; owners of racing participants such as cars, dogs, horses, etc. primarily engaged in entering them in racing events or other spectator sports events; sports trainers providing specialised services to support participants in sports events or competitions; operators of arenas and stadiums; other activities of organising, promoting or managing sports events, n.e.c. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399428
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				93.11 
			</td>
		

			<td valign='top'>
				93.1 
			</td>
		

			<td valign='top'>
				Operation of sports facilities 
			</td>
		

			<td valign='top'>
				9311 
			</td>
		

			<td valign='top'>
				This class includes:- operation of facilities for indoor or outdoor sports events (open, closed or covered, with or without spectator seating):  • football, hockey, cricket, rugby stadiums  • racetracks for car, dog, horse races  • swimming pools and stadiums  • track and field stadiums  • winter sports arenas and stadiums  • ice-hockey arenas  • boxing arenas  • golf courses  • bowling lanes- organisation and operation of outdoor or indoor sports events for professionals or amateurs by organisations with own facilities 
			</td>
		

			<td valign='top'>
				This class includes managing and providing the staff to operate these facilities. 
			</td>
		

			<td valign='top'>
				- Operation of squash facilities 
			</td>
		

			<td valign='top'>
				This class excludes:- operation of ski lifts, see 49.39- rental of recreation and sports equipment, see 77.21- activities of fitness facilities, see 93.13- park and beach activities, see 93.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399429
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				93.12 
			</td>
		

			<td valign='top'>
				93.1 
			</td>
		

			<td valign='top'>
				Activities of sports clubs 
			</td>
		

			<td valign='top'>
				9312 
			</td>
		

			<td valign='top'>
				This class includes the activities of sports clubs, which, whether professional, semi-professional or amateur clubs, give their members the opportunity to engage in sporting activities.This class includes:- operation of sports clubs:  • football clubs  • bowling clubs  • swimming clubs  • golf clubs  • boxing clubs  • winter sports clubs  • chess clubs  • track and field clubs  • shooting clubs, etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Advertising space sale, by sports clubs- Majorette activities, as part of sports events 
			</td>
		

			<td valign='top'>
				This class excludes:- sports instruction by individual teachers, trainers, see 85.51- operation of sports facilities, see 93.11- organisation and operation of outdoor or indoor sports events for professionals or amateurs by sports clubs with their own facilities, see 93.11 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399430
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				93.13 
			</td>
		

			<td valign='top'>
				93.1 
			</td>
		

			<td valign='top'>
				Fitness facilities 
			</td>
		

			<td valign='top'>
				9311 
			</td>
		

			<td valign='top'>
				This class includes:- fitness and body-building clubs and facilities 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- sports instruction by individual teachers, trainers, see 85.51 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399431
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				93.19 
			</td>
		

			<td valign='top'>
				93.1 
			</td>
		

			<td valign='top'>
				Other sports activities 
			</td>
		

			<td valign='top'>
				9319 
			</td>
		

			<td valign='top'>
				This class includes:- activities of producers or promoters of sports events, with or without facilities- activities of individual own-account sportsmen and athletes, referees, judges, timekeepers etc.- activities of sports leagues and regulating bodies- activities related to promotion of sporting events- activities of racing stables, kennels and garages- operation of sport fishing and hunting preserves- activities of mountain guides- support activities for sport or recreational hunting and fishing 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Provision of personnel and equipment for sporting events- Advertising space sale, by racing stables- Operation of vertical, aerodynamic wind tunnels 
			</td>
		

			<td valign='top'>
				This class excludes:- rental of sports equipment, see 77.21- activities of sport and game schools, see 85.51- activities of sports instructors, teachers, coaches, see 85.51- organisation and operation of outdoor or indoor sports events for professionals or amateurs by sports clubs with/without own facilities, see 93.11, 93.12- park and beach activities, see 93.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399432
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				93.2 
			</td>
		

			<td valign='top'>
				93 
			</td>
		

			<td valign='top'>
				Amusement and recreation activities 
			</td>
		

			<td valign='top'>
				932 
			</td>
		

			<td valign='top'>
				This group includes a wide range of units that operate facilities or provide services to meet the varied recreational interests of their patrons. It includes the operation of a variety of attractions, such as mechanical rides, water rides, games, shows, theme exhibits and picnic grounds. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This group excludes sports activities and dramatic arts, music and other arts and entertainment. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399433
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				93.21 
			</td>
		

			<td valign='top'>
				93.2 
			</td>
		

			<td valign='top'>
				Activities of amusement parks and theme parks 
			</td>
		

			<td valign='top'>
				9321 
			</td>
		

			<td valign='top'>
				This class includes activities of amusement parks or theme parks. It includes the operation of a variety of attractions, such as mechanical rides, water rides, games, shows, theme exhibits and picnic grounds. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Activities of aqua/water parks 
			</td>
		 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399434
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				93.29 
			</td>
		

			<td valign='top'>
				93.2 
			</td>
		

			<td valign='top'>
				Other amusement and recreation activities 
			</td>
		

			<td valign='top'>
				9329 
			</td>
		

			<td valign='top'>
				This class includes activities related to entertainment and recreation (except amusement parks and theme parks) not elsewhere classified:- operation (exploitation) of coin-operated games- activities of recreation parks (without accommodation)- operation of recreational transport facilities, e.g. marinas- operation of ski hills- rental of leisure and pleasure equipment as an integral part of recreational facilities- fairs and shows of a recreational nature- activities of beaches, including rental of facilities such as bathhouses, lockers, chairs etc.- operation of dance floors 
			</td>
		

			<td valign='top'>
				This class also includes activities of producers or entrepreneurs of live events other than arts or sports events, with or without facilities. 
			</td>
		

			<td valign='top'>
				- Operation of computer rooms for playing computer games- Operation of self-service rocking chairs/horses/cars/space ships etc. as amusement/recreation devices- Airsoft and paintball 
			</td>
		

			<td valign='top'>
				This class excludes:- operation of teleferics, funiculars, ski and cable lifts, see 49.39- fishing cruises, see 50.10, 50.30- provision of space and facilities for short stay by visitors in recreational parks and forests and campgrounds, see 55.30- trailer parks, recreational camps, hunting and fishing camps, campsites and campgrounds, see 55.30- discotheques, see 56.30- theatrical and circus groups, see 90.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399435
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				S 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				OTHER SERVICE ACTIVITIES 
			</td>
		

			<td valign='top'>
				S 
			</td>
		

			<td valign='top'>
				This section (as a residual category) includes the activities of membership organisations, the repair of computers and personal and household goods and a variety of personal service activities not covered elsewhere in the classification. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399436
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				94 
			</td>
		

			<td valign='top'>
				S 
			</td>
		

			<td valign='top'>
				Activities of membership organisations 
			</td>
		

			<td valign='top'>
				94 
			</td>
		

			<td valign='top'>
				This division includes activities of organisations representing interests of special groups or promoting ideas to the general public. These organisations usually have a constituency of members, but their activities may involve and benefit non-members as well. The primary breakdown of this division is determined by the purpose that these organisations serve, namely interests of employers, self-employed individuals and the scientific community (group 94.1), interests of employees (group 94.2) or promotion of religious, political, cultural, educational or recreational ideas and activities (group 94.9). 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399437
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				94.1 
			</td>
		

			<td valign='top'>
				94 
			</td>
		

			<td valign='top'>
				Activities of business, employers and professional membership organisations 
			</td>
		

			<td valign='top'>
				941 
			</td>
		

			<td valign='top'>
				This group includes the activities of units that promote the interests of the members of business and employers organisations. 
			</td>
		

			<td valign='top'>
				In the case of professional membership organisations, it also includes the activities of promoting the professional interests of members of the profession. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399438
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				94.11 
			</td>
		

			<td valign='top'>
				94.1 
			</td>
		

			<td valign='top'>
				Activities of business and employers membership organisations 
			</td>
		

			<td valign='top'>
				9411 
			</td>
		

			<td valign='top'>
				This class includes:- activities of organisations whose members' interests centre on the development and prosperity of enterprises in a particular line of business or trade, including farming, or on the economic growth and climate of a particular geographical area or political subdivision without regard for the line of business.- activities of federations of such associations- activities of chambers of commerce, guilds and similar organisations- dissemination of information, representation before government agencies, public relations and labour negotiations of business and employer organisations 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- activities of trade unions, see 94.20 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399439
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				94.12 
			</td>
		

			<td valign='top'>
				94.1 
			</td>
		

			<td valign='top'>
				Activities of professional membership organisations 
			</td>
		

			<td valign='top'>
				9412 
			</td>
		

			<td valign='top'>
				This class includes:- activities of organisations whose members' interests centre chiefly on a particular scholarly discipline or professional practice or technical field, such as medical associations, legal associations, accounting associations, engineering associations, architects associations etc.- activities of associations of specialists engaged in scientific, academic or cultural activities, such as associations of writers, painters, performers of various kinds, journalists etc.- dissemination of information, the establishment and supervision of standards of practice, representation before government agencies and public relations of professional organisations 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of learned societies 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- education provided by these organisations, see division 85 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399440
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				94.2 
			</td>
		

			<td valign='top'>
				94 
			</td>
		

			<td valign='top'>
				Activities of trade unions 
			</td>
		

			<td valign='top'>
				942 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399441
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				94.20 
			</td>
		

			<td valign='top'>
				94.2 
			</td>
		

			<td valign='top'>
				Activities of trade unions 
			</td>
		

			<td valign='top'>
				9420 
			</td>
		

			<td valign='top'>
				This class includes:- promoting of the interests of organised labour and union employees 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of associations whose members are employees interested chiefly in the representation of their views concerning the salary and work situation, and in concerted action through organisation- activities of single plant unions, of unions composed of affiliated branches and of labour organisations composed of affiliated unions on the basis of trade, region, organisational structure or other criteria 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- education provided by such organisations, see division 85 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399442
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				94.9 
			</td>
		

			<td valign='top'>
				94 
			</td>
		

			<td valign='top'>
				Activities of other membership organisations 
			</td>
		

			<td valign='top'>
				949 
			</td>
		

			<td valign='top'>
				This group includes the activities of units (except business and employers organisations, professional organisations, trade unions) that promote the interests of their members. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399443
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				94.91 
			</td>
		

			<td valign='top'>
				94.9 
			</td>
		

			<td valign='top'>
				Activities of religious organisations 
			</td>
		

			<td valign='top'>
				9491 
			</td>
		

			<td valign='top'>
				This class includes:- activities of religious organisations or individuals providing services directly to worshippers in churches, mosques, temples, synagogues or other places- activities of organisations furnishing monastery and convent services- religious retreat activities 
			</td>
		

			<td valign='top'>
				This class also includes:- religious funeral service activities 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- education provided by such organisations, see division 85- health activities by such organisations, see division 86- social work activities by such organisations, see divisions 87, 88 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399444
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				94.92 
			</td>
		

			<td valign='top'>
				94.9 
			</td>
		

			<td valign='top'>
				Activities of political organisations 
			</td>
		

			<td valign='top'>
				9492 
			</td>
		

			<td valign='top'>
				This class includes:- activities of political organisations and auxiliary organisations such as young people's auxiliaries associated with a political party. These organisations chiefly engage in influencing decision-taking in public governing bodies by placing members of the party or those sympathetic to the party in political office and involve the dissemination of information, public relations, fund-raising etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399445
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				94.99 
			</td>
		

			<td valign='top'>
				94.9 
			</td>
		

			<td valign='top'>
				Activities of other membership organisations n.e.c. 
			</td>
		

			<td valign='top'>
				9499 
			</td>
		

			<td valign='top'>
				This class includes:- activities of organisations (not directly affiliated to a political party) furthering a public cause or issue by means of public education, political influence, fund-raising etc.:  • citizens initiative or protest movements  • environmental and ecological movements  • organisations supporting community and educational facilities n.e.c.  • organisations for the protection and betterment of special groups, e.g. ethnic and minority groups  • associations for patriotic purposes, including war veterans' associations- consumer associations- automobile associations- associations for the purpose of social acquaintanceship such as rotary clubs, lodges etc.- associations of youth, young persons' associations, student associations, clubs and fraternities etc.- associations for the pursuit of a cultural or recreational activity or hobby (other than sports or games), e.g. poetry, literature and book clubs, historical clubs, gardening clubs, film and photo clubs, music and art clubs, craft and collectors' clubs, social clubs, carnival clubs etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- grant giving activities by membership organisations or others 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				This class excludes:- charitable activities like fund-raising aimed at social work, see 88.99- activities of professional artistic groups or organisations, see 90.0- activities of sports clubs, see 93.12- activities of professional associations, see 94.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399446
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				95 
			</td>
		

			<td valign='top'>
				S 
			</td>
		

			<td valign='top'>
				Repair of computers and personal and household goods 
			</td>
		

			<td valign='top'>
				95 
			</td>
		

			<td valign='top'>
				This division includes the repair and maintenance of computers peripheral equipment such as desktops, laptops, computer terminals, storage devices and printers. 
			</td>
		

			<td valign='top'>
				It also includes the repair of communications equipment such as fax machines, two-way radios and consumer electronics such as radios and TVs, home and garden equipment such as lawn-mowers and blowers, footwear and leather goods, furniture and home furnishings, clothing and clothing accessories, sporting goods, musical instruments, hobby articles and other personal and household goods. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				Excluded from this division is the repair of medical and diagnostic imaging equipment, measuring and surveying instruments, laboratory instruments, radar and sonar equipment, see 33.13. 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399447
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				95.1 
			</td>
		

			<td valign='top'>
				95 
			</td>
		

			<td valign='top'>
				Repair of computers and communication equipment 
			</td>
		

			<td valign='top'>
				951 
			</td>
		

			<td valign='top'>
				This group includes the repair and maintenance of computers and peripheral equipment and communications equipment. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399448
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.11 
			</td>
		

			<td valign='top'>
				95.1 
			</td>
		

			<td valign='top'>
				Repair of computers and peripheral equipment 
			</td>
		

			<td valign='top'>
				9511 
			</td>
		

			<td valign='top'>
				This class includes the repair of electronic equipment, such as computers and computing machinery and peripheral equipment.This class includes the repair and maintenance of:- desktop computers- laptop computers- magnetic disk drives, flash drives and other storage devices- optical disk drives (CD-RW, CD-ROM, DVD-ROM, DVD-RW)- printers- monitors- keyboards- mice, joysticks and trackball accessories- internal and external computer modems- dedicated computer terminals- computer servers- scanners, including bar code scanners- smart card readers- virtual reality helmets- computer projectors 
			</td>
		

			<td valign='top'>
				This class also includes the repair and maintenance of:- computer terminals like automatic teller machines (ATM's); point-of-sale (POS) terminals, not mechanically operated- hand-held computers (PDA's) 
			</td>
		

			<td valign='top'>
				- Renovation of CDs and DVDs 
			</td>
		

			<td valign='top'>
				This class excludes:- the repair and maintenance of carrier equipment modems, see 95.12 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399449
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.12 
			</td>
		

			<td valign='top'>
				95.1 
			</td>
		

			<td valign='top'>
				Repair of communication equipment 
			</td>
		

			<td valign='top'>
				9512 
			</td>
		

			<td valign='top'>
				This class includes repair and maintenance of communications equipment such as:- cordless telephones- cellular phones- carrier equipment modems- fax machines- communications transmission equipment (e.g. routers, bridges, modems)- two-way radios- commercial TV and video cameras 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399450
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				95 
			</td>
		

			<td valign='top'>
				Repair of personal and household goods 
			</td>
		

			<td valign='top'>
				952 
			</td>
		

			<td valign='top'>
				This group includes the repair and servicing of personal and household goods. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399451
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.21 
			</td>
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				Repair of consumer electronics 
			</td>
		

			<td valign='top'>
				9521 
			</td>
		

			<td valign='top'>
				This class includes repair and maintenance of consumer electronics:- repair of consumer electronics:  • television, radio receivers  • video cassette recorders (VCR)  • CD players  • household-type video cameras 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399452
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.22 
			</td>
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				Repair of household appliances and home and garden equipment 
			</td>
		

			<td valign='top'>
				9522 
			</td>
		

			<td valign='top'>
				This class includes the repair and servicing household appliances and home and garden equipment:- repair and servicing of household appliances  • refrigerators, stoves, washing machines, clothes dryers, room air conditioners, etc.- repair and servicing of home and garden equipment  • lawnmowers, edgers, snow- and leaf- blowers, trimmers, etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- repair of hand held power tools, see 33.12- repair of central air conditioning systems, see 43.22 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399453
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.23 
			</td>
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				Repair of footwear and leather goods 
			</td>
		

			<td valign='top'>
				9523 
			</td>
		

			<td valign='top'>
				This class includes repair and maintenance of footwear and leather goods:- repair of boots, shoes, luggage and the like- fitting of heels 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399454
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.24 
			</td>
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				Repair of furniture and home furnishings 
			</td>
		

			<td valign='top'>
				9524 
			</td>
		

			<td valign='top'>
				This class includes:- reupholstering, refinishing, repairing and restoring of furniture and home furnishings including office furniture 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399455
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.25 
			</td>
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				Repair of watches, clocks and jewellery 
			</td>
		

			<td valign='top'>
				9529 
			</td>
		

			<td valign='top'>
				This class includes:- repair of watches, clocks and their parts such as watch cases and housings of all materials; movements, chronometers, etc.- repair of jewellery 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- repair of time clocks, time/date stamps, time locks and similar time recording devices, see 33.13 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399456
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				95.29 
			</td>
		

			<td valign='top'>
				95.2 
			</td>
		

			<td valign='top'>
				Repair of other personal and household goods 
			</td>
		

			<td valign='top'>
				9529 
			</td>
		

			<td valign='top'>
				This class includes repair of personal and household goods:- repair of bicycles- repair and alteration of clothing- repair of sporting goods (except sporting guns) and camping equipment- repair of books- repair of musical instruments (except organs and historical musical instruments)- repair of toys and similar articles- repair of other personal and household goods- piano-tuning 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- industrial engraving of metals, see 25.61- repair of sporting and recreational guns, 33.11- repair of hand held power tools, see 33.12- restoring of organs and other historical musical instruments, see 33.19 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399457
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				96 
			</td>
		

			<td valign='top'>
				S 
			</td>
		

			<td valign='top'>
				Other personal service activities 
			</td>
		

			<td valign='top'>
				96 
			</td>
		

			<td valign='top'>
				This division includes all service activities not mentioned elsewhere in the classification. Notably it includes types of services such as washing and (dry-)cleaning of textiles and fur products, hairdressing and other beauty treatment, funeral and related activities. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399458
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				96.0 
			</td>
		

			<td valign='top'>
				96 
			</td>
		

			<td valign='top'>
				Other personal service activities 
			</td>
		

			<td valign='top'>
				960 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399459
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				96.01 
			</td>
		

			<td valign='top'>
				96.0 
			</td>
		

			<td valign='top'>
				Washing and (dry-)cleaning of textile and fur products 
			</td>
		

			<td valign='top'>
				9601 
			</td>
		

			<td valign='top'>
				This class includes:- laundering and dry cleaning, pressing etc., of all kinds of clothing (including fur) and textiles, provided by mechanical equipment, by hand or by self-service coin-operated machines, whether for the general public or for industrial or commercial clients- laundry collection and delivery- carpet and rug shampooing and drapery and curtain cleaning, whether on clients' premises or not- provision of linens, work uniforms and related items by laundries- diaper supply services 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- rental of clothing other than work uniforms, even if cleaning of these goods is an integral part of the activity, see 77.29- repair and alteration of clothing, see 95.29 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399460
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				96.02 
			</td>
		

			<td valign='top'>
				96.0 
			</td>
		

			<td valign='top'>
				Hairdressing and other beauty treatment 
			</td>
		

			<td valign='top'>
				9602 
			</td>
		

			<td valign='top'>
				This class includes:- hair washing, trimming and cutting, setting, dyeing, tinting, waving, straightening and similar activities for men and women- shaving and beard trimming- facial massage, manicure and pedicure, make-up etc. 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Hair design- Permanent make-up 
			</td>
		

			<td valign='top'>
				This class excludes:- manufacture of wigs, see 32.99 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399461
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				96.03 
			</td>
		

			<td valign='top'>
				96.0 
			</td>
		

			<td valign='top'>
				Funeral and related activities 
			</td>
		

			<td valign='top'>
				9603 
			</td>
		

			<td valign='top'>
				This class includes:- burial and incineration of human or animal corpses and related activities:  • preparing the dead for burial or cremation and embalming and morticians' services  • providing burial or cremation services  • rental of equipped space in funeral parlours- rental or sale of graves- maintenance of graves and mausoleums 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- cemetery gardening, see 81.30- religious funeral service activities, see 94.91 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399462
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				96.04 
			</td>
		

			<td valign='top'>
				96.0 
			</td>
		

			<td valign='top'>
				Physical well-being activities 
			</td>
		

			<td valign='top'>
				9609 
			</td>
		

			<td valign='top'>
				This class includes:- activities of Turkish baths, sauna and steam baths, solariums, reducing and slendering salons, massage salons etc. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- medical massage and therapy, see 86.90- activities of health, fitness and body-building clubs and facilities, see 93.13 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399463
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				96.09 
			</td>
		

			<td valign='top'>
				96.0 
			</td>
		

			<td valign='top'>
				Other personal service activities n.e.c. 
			</td>
		

			<td valign='top'>
				9609 
			</td>
		

			<td valign='top'>
				This class includes:- astrological and spiritualists' activities- social activities such as escort services, dating services, services of marriage bureaux- pet care services such as boarding, grooming, sitting and training pets- genealogical organisations- activities of tattooing and piercing studios- shoe shiners, porters, valet car parkers etc.- concession operation of coin-operated personal service machines (photo booths, weighing machines, machines for checking blood pressure, coin-operated lockers etc.) 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				- Dating and other speed networking activities- Tattoo artist activities, using biological substances (such as henna) for temporary decoration- Horse whispering activities- Housesitting activities- Healer activities 
			</td>
		

			<td valign='top'>
				This class excludes:- veterinary activities, see 75.00- coin-operated gambling machines, see 92.00- coin-operated washing machines, see 96.01 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399464
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				T 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				ACTIVITIES OF HOUSEHOLDS AS EMPLOYERS; UNDIFFERENTIATED GOODS- AND SERVICES-PRODUCING ACTIVITIES OF HOUSEHOLDS FOR OWN USE 
			</td>
		

			<td valign='top'>
				T 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399465
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				97 
			</td>
		

			<td valign='top'>
				T 
			</td>
		

			<td valign='top'>
				Activities of households as employers of domestic personnel 
			</td>
		

			<td valign='top'>
				97 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399466
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				97.0 
			</td>
		

			<td valign='top'>
				97 
			</td>
		

			<td valign='top'>
				Activities of households as employers of domestic personnel 
			</td>
		

			<td valign='top'>
				970 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399467
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				97.00 
			</td>
		

			<td valign='top'>
				97.0 
			</td>
		

			<td valign='top'>
				Activities of households as employers of domestic personnel 
			</td>
		

			<td valign='top'>
				9700 
			</td>
		

			<td valign='top'>
				This class includes the activities of households as employers of domestic personnel such as maids, cooks, waiters, valets, butlers, laundresses, gardeners, gatekeepers, stable-lads, chauffeurs, caretakers, governesses, babysitters, tutors, secretaries etc. It allows the domestic personnel employed to state the activity of their employer in censuses or studies, even though the employer is an individual. The product produced by this activity is consumed by the employing household. 
			</td>
		 
				<td></td>
				 
				<td></td>
				

			<td valign='top'>
				This class excludes:- provision of services such as cooking, gardening etc. by independent service providers (companies or individuals), see according to type of service 
			</td>
		

	</tr>

	<tr>
		<td valign='top'>
			399468
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				98 
			</td>
		

			<td valign='top'>
				T 
			</td>
		

			<td valign='top'>
				Undifferentiated goods- and services-producing activities of private households for own use 
			</td>
		

			<td valign='top'>
				98 
			</td>
		

			<td valign='top'>
				This division includes the undifferentiated subsistence goods-producing and services-producing activities of households. Households should be classified here only if it is impossible to identify a primary activity for the subsistence activities of the household. If the household engages in market activities, it should be classified according to the primary market activity carried out. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399469
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				98.1 
			</td>
		

			<td valign='top'>
				98 
			</td>
		

			<td valign='top'>
				Undifferentiated goods-producing activities of private households for own use 
			</td>
		

			<td valign='top'>
				981 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399470
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				98.10 
			</td>
		

			<td valign='top'>
				98.1 
			</td>
		

			<td valign='top'>
				Undifferentiated goods-producing activities of private households for own use 
			</td>
		

			<td valign='top'>
				9810 
			</td>
		

			<td valign='top'>
				This class includes the undifferentiated subsistence goods-producing activities of households, that is to say, the activities of households that are engaged in a variety of activities that produce goods for their own subsistence. These activities include hunting and gathering, farming, the production of shelter and clothing and other goods produced by the household for its own subsistence. If households are also engaged in the production of marketed goods, they are classified to the appropriate goods-producing industry of NACE. If they are principally engaged in a specific goods-producing subsistence activity, they are classified to the appropriate goods-producing industry of NACE. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399471
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				98.2 
			</td>
		

			<td valign='top'>
				98 
			</td>
		

			<td valign='top'>
				Undifferentiated service-producing activities of private households for own use 
			</td>
		

			<td valign='top'>
				982 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399472
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				98.20 
			</td>
		

			<td valign='top'>
				98.2 
			</td>
		

			<td valign='top'>
				Undifferentiated service-producing activities of private households for own use 
			</td>
		

			<td valign='top'>
				9820 
			</td>
		

			<td valign='top'>
				This class includes the undifferentiated subsistence services-producing activities of households. These activities include cooking, teaching, caring for household members and other services produced by the household for its own subsistence. If households are also engaged in the production of multiple goods for subsistence purposes, they are classified to the undifferentiated goods-producing subsistence activities of households. 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399473
		</td>
		<td valign='top'>
			1
		</td>

		
		

			<td valign='top'>
				U 
			</td>
		 
				<td></td>
				

			<td valign='top'>
				ACTIVITIES OF EXTRATERRITORIAL ORGANISATIONS AND BODIES 
			</td>
		

			<td valign='top'>
				U 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399474
		</td>
		<td valign='top'>
			2
		</td>

		
		

			<td valign='top'>
				99 
			</td>
		

			<td valign='top'>
				U 
			</td>
		

			<td valign='top'>
				Activities of extraterritorial organisations and bodies 
			</td>
		

			<td valign='top'>
				99 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399475
		</td>
		<td valign='top'>
			3
		</td>

		
		

			<td valign='top'>
				99.0 
			</td>
		

			<td valign='top'>
				99 
			</td>
		

			<td valign='top'>
				Activities of extraterritorial organisations and bodies 
			</td>
		

			<td valign='top'>
				990 
			</td>
		 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				 
				<td></td>
				

	</tr>

	<tr>
		<td valign='top'>
			399476
		</td>
		<td valign='top'>
			4
		</td>

		
		

			<td valign='top'>
				99.00 
			</td>
		

			<td valign='top'>
				99.0 
			</td>
		

			<td valign='top'>
				Activities of extraterritorial organisations and bodies 
			</td>
		

			<td valign='top'>
				9900 
			</td>
		

			<td valign='top'>
				This class includes:- activities of international organisations such as the United Nations and the specialised agencies of the United Nations system, regional bodies etc., the International Monetary Fund, the World Bank, the World Customs Organisation, the Organisation for Economic Co-operation and Development, the organisation of Petroleum Exporting Countries, the European Communities, the European Free Trade Association etc. 
			</td>
		

			<td valign='top'>
				This class also includes:- activities of diplomatic and consular missions when being determined by the country of their location rather than by the country they represent 
			</td>
		 
				<td></td>
				 
				<td></td>
				

	</tr>

			</table>
			</body>
			</html>
";
    }
}
