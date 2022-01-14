using System;

/**
 * 
 * Die Klasse ErrorViewModel besitzt folgende Attribute:
 * - RequestId
 * - ShowRequestId
 * 
 * ShowRequestId überprüft ob die RequestId null oder empty ist.
 * 
 * 
 * @Author Jose Panov
 * @Version 2022.01.01
 */


namespace TimeChecker.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
