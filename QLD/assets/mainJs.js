window.onload=function(){
    
    var listCheckbox=document.querySelectorAll('.selectTable table tbody td input')

    for(let i=0; i<listCheckbox.length; i++){
            if(listCheckbox[i].checked){
                document.querySelector('.selectColumn').style.background='#ccc'
                
            }
    }  


    //-----------------------------------------------------------------CheckColumn
    
    
    
    var checkboxColumn=document.querySelectorAll('#MainContent_CheckBoxListColumn tbody tr td input')
    let dem=0
    for(let i=0; i<checkboxColumn.length; i++){
        if(checkboxColumn[i].checked==true){
            dem++
        }
    }
    if(dem>0){
        document.getElementById('query-content').style.display='block'
    }


    // ----------------------------------------------------------------Gridview
    var listCheckOrder =document.querySelectorAll("#query-content .columnOrder input[type='checkbox'" )
    var listOrder  =document.querySelectorAll("#query-content .columnOrder .textOrder")
    let countOrder=0;
    // localStorage.setItem('checked',0)
    // localStorage.setItem('checked',countOrder)
    var selectText=''
    var orderObject=new Array()
    for(let i=0; i<listCheckOrder.length; i++){    
        listCheckOrder[i].addEventListener('change',function(){ 
            if(this.checked== true){
                countOrder++
                listOrder[i].innerHTML=countOrder.toString()
                orderObject.push(i)
                selectText=orderObject.join(',')
                localStorage.setItem('select',selectText)
                localStorage.setItem('checked',countOrder)
            }
            else{
                if(countOrder>0) countOrder--
                listOrder[i].innerHTML=''
                localStorage.setItem('checked',countOrder)
                map.delete(i.toString())
            }
        })
        
    }
    var orderSelect=document.querySelector('.orderSelect span')
    var temp=localStorage.getItem('select')
    const map =new Map()
    if(temp!=''){
        
        var tempArr=temp.split(',')
        var selectArr=orderSelect.textContent.split(', ')
        for(let i=0; i<tempArr.length; i++){
            map.set(tempArr[i],selectArr[i])
        }
        
    }
    var orderSelectChange=document.getElementById('MainContent_total')
    var selectButton=document.getElementById('MainContent_LabelMess')
    var tempArr2=new Array()
    let countTemp=0
    if(selectButton.textContent!=''){
        tempArr2=selectButton.textContent.split('FROM')
        console.log(tempArr2)
    }
    
    for(let i=0; i<tempArr.length; i++){
        countTemp++
        if(selectButton.textContent!=''){
            orderSelectChange.style.display='block'
            document.getElementById('MainContent_fixSelect').style.display='block'
            document.getElementById('MainContent_fixSelect').innerHTML=''
            document.getElementById('MainContent_titleTexbox').innerHTML=''
            document.getElementById('MainContent_Producer').innerHTML=''
            if(localStorage.getItem('checked')==listCheckOrder.length){
                if(countTemp==tempArr.length){
                    orderSelectChange.innerHTML+=map.get(i.toString())
                    orderSelectChange.innerHTML='SELECT '+orderSelectChange.innerHTML+' FROM '+tempArr2[1]
                    break
                }
                orderSelectChange.innerHTML+=map.get(i.toString())+', '
            }
            if(orderSelectChange.innerHTML!=''){
                document.querySelector('.orderSelectChange small').style.display='block'
            }
            // window.localStorage.removeItem('checked  ')
        }
    }

    function CopyToClipboard(id)
    {
        var r = document.createRange();
        r.selectNode(document.getElementById(id));
        window.getSelection().removeAllRanges();
        window.getSelection().addRange(r);
        document.execCommand('copy');
        window.getSelection().removeAllRanges();
    }
    function copyToClipboard(element) {
        var $temp = $("<input>");
        $("body").append($temp);
        $temp.val($(element).text()).select();
        document.execCommand("copy");
        $temp.remove();
      }
    
}





