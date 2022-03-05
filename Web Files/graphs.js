
function getValue(){
    const Toast = Swal.mixin({
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 2000,
      })
   
      
    var ates,nabiz,sicaklik;
    var roomTempLower,roomTempUpper,patientTempLower,patientTempUpper,pulseLower,pulseUpper;
    Plotly.plot('graph', [{
        y: [],
        mode: 'lines',
        line: {
            color: '#E50914'
        }
    }]);
    Plotly.plot('graph2', [{
        y: [],
        mode: 'lines',
        line: {
            color: '#E50914'
        }
    }]);
    Plotly.plot('graph3', [{
        y: [],
        mode: 'lines',
        line: {
            color: '#E50914'
        }
    }]);
    setInterval(function () {
        $.ajax({
            type: "POST",
            url: "php/getValue.php",
            success: function(data) {
                veri=JSON.parse(data);
                nabiz=veri.nabiz;
                ates=veri.ates;
                sicaklik=veri.sicaklik;
                roomTempLower=veri.roomTempLower;
                roomTempUpper=veri.roomTempUpper;
                patientTempUpper=veri.patientTempUpper;
                patientTempLower=veri.patientTempLower;
                pulseLower=veri.pulseLower;
                pulseUpper=veri.pulseUpper;
            }
        });
        Plotly.extendTraces('graph', {
            y: [
                [ates]
            ]
        }, [0])
        Plotly.extendTraces('graph2', {
            y: [
                [sicaklik]
            ]
        }, [0])
        Plotly.extendTraces('graph3', {
            y: [
                [nabiz]
            ]
        }, [0])
            if(ates>patientTempUpper || ates<patientTempLower){
                Toast.fire({
                    icon: 'warning',
                    title: `Ateş: ${ates} Küvez Sıcaklığı: ${sicaklik}, Nabız: ${nabiz}, Kişinin ateşini kontrol ediniz. `,
                })
            }
            if(sicaklik>roomTempUpper || sicaklik<roomTempLower){
                Toast.fire({
                    icon: 'warning',
                    title: `Ateş: ${ates} Küvez Sıcaklığı: ${sicaklik}, Nabız: ${nabiz}, Küvez sıcaklığını kontrol ediniz. `,
                })
            }
            if(nabiz>pulseUpper || nabiz<pulseLower){
                Toast.fire({
                    icon: 'warning',
                    title: `Ateş: ${ates} Küvez Sıcaklığı: ${sicaklik}, Nabız: ${nabiz}, Kişi nabzını kontrol ediniz. `,
                })
            }
            
            
        
        
    }, 1000);
}
$( document ).ready(function() {
    getValue();
});
