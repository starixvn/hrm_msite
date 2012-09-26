/**
 * autotab
 * 
 * The attr() method doesn't work when trying to set or return the value of the 'tabindex' in IE so 
 * we have to use $(selector).get(index).tabIndex for setting and returning. This might be only in 
 * the case when the tabIndex not predefined but added after the page loads via Javascript.
 * 
 * @author  Trey Shugart
 * @date    2008-11-12
 * @version 0.9.0
 * 
 * @requiredPlugins
 *     caret
 *     keys
 * 
 * @license
 *     Copyright (c) 2009, Trey Shugart
 *     All rights reserved.
 *     
 *     Redistribution and use in source and binary forms, with or without modification, are permitted 
 *     provided that the following conditions are met:
 *     
 *     Redistributions of source code must retain the above copyright notice, this list of conditions 
 *     and the following disclaimer.
 *     
 *     Redistributions in binary form must reproduce the above copyright notice, this list of conditions 
 *     and the following disclaimer in the documentation and/or other materials provided with the 
 *     distribution.
 *     
 *     Neither the name of the the organization nor the names of its contributors may be used to endorse 
 *     or promote products derived from this software without specific prior written permission.
 *     
 *     THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR 
 *     IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY 
 *     AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER 
 *     OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
 *     CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
 *     SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY 
 *     THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR 
 *     OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
 *     POSSIBILITY OF SUCH DAMAGE.
 */
;(function($) {
	
	$.fn.extend({
		autoTab : function() {
			$$ = $(this);
			
			$$.each(function() {
				var i = $(this);
				
				i.keydown(function(e) {
					var k = $.keyCode(e);
					
					if (i.val().length === 0 && k === 8)
						$$
							.filter(function(index) {
								return parseInt($$.get(index).tabIndex) == parseInt(i.get(0).tabIndex) - 1;
							})
							.focus()
							.caretToEnd();
					if (i.val().length === parseInt(i.attr('maxlength')) && k !== 8 && (k >= (65 || 96 || 186) && k <= (90 || 111 || 222)))
						$$
							.filter(function(index) {
								return parseInt($$.get(index).tabIndex) == parseInt(i.get(0).tabIndex) + 1;
							})
							.focus();
				});
			});
			
			return $(this);
		},
		
		autoTabIndex: function() {
			$$ = $(this);
			
			for (var i = 0; i < $$.length; i++)
				$$.get(i).tabIndex = i+1;
		}
	});

})(jQuery);