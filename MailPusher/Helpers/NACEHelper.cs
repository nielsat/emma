using MailPusher.Models;
using MailPusher.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MailPusher.Helpers
{
    public class NACEHelper
    {
        public List<NACE> GetAll()
        {
            NACERepo repo = new NACERepo();
            return Map(repo.GetAllRecords());
        }

        public List<NACE> GetPublisherNaces()
        {
            NACERepo repo = new NACERepo();
            return Map(repo.GetPublisherNaces());
        }

        public List<NACE> Map(List<Repository.Models.NACE> naces)
        {
            List<NACE> result = new List<NACE>();
            foreach (var item in naces)
            {
                result.Add(Map(item));
            }
            return result;
        }

        public NACE Map(Repository.Models.NACE nace)
        {
            return new NACE()
            {
                ID = nace.ID,
                Description = nace.Description,
                Excludes = nace.Excludes,
                Includes = nace.Includes,
                Rulings = nace.Rulings,
                ParentCode = nace.ParentCode,
                Code = nace.Code
            };
        }

        public List<TreeNode> GenerateTree(List<NACE> naceData)
        {
            List<TreeNode> result = new List<TreeNode>();
            string tooltipTemplate = @"
                <table class='naceTree'>
                    <tbody>
                        <tr>
                            <td>Code</td>
                            <td>{0}</td>
                        </tr>
                        <tr>
                            <td>Includes &nbsp;&nbsp;&nbsp;</td>
                            <td>{1}</td>
                        </tr>
                        <tr>
                            <td>Excludes</td>
                            <td>{2}</td>
                        </tr>
                        <tr>
                            <td>Rulings</td>
                            <td>{3}</td>
                        </tr>
                    </tbody>
                </table>
            ";
            foreach (var nace in naceData)
            {
                result.Add(new TreeNode()
                {
                    parent = string.IsNullOrEmpty(nace.ParentCode) ? "#" : nace.ParentCode,
                    id = nace.Code,
                    text = nace.Description,
                    a_attr = new TreeNodeAttr() {
                        @class = "treeTooltip",
                        title = string.Format(tooltipTemplate, nace.Code, nace.Includes, nace.Excludes, nace.Rulings),
                        id = nace.ID.ToString()
                    }
                });
            }
            return result;
        }
    }
}