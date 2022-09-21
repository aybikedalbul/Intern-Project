const raster = new ol.layer.Tile({
    source: new ol.source.OSM(),
  });
  
  const source = new ol.source.Vector({
    wrapX: false
  });
  
  const vector = new ol.layer.Vector({
    source: source,
  })
  
  const map = new ol.Map({
      target: 'map',
      layers: [raster, vector],
      view: new ol.View({
        center: ol.proj.fromLonLat([35,39]),
        zoom: 7
      })
    });
  
  const typeSelect = document.getElementById('type');
  
  function handleBtnClick(){
    map.removeInteraction(draw);
    addInteraction();
  };

  let draw;
  function addInteraction(){
    if(typeSelect.value != "None"){
      draw = new ol.interaction.Draw({
        source: source,
        type: typeSelect.value,
      });
      map.addInteraction(draw);
    }

    draw.on('drawend',
          function(evt) {
            var feature = evt.feature;
            var coords = feature.getGeometry().getCoordinates();
            var x = coords[0]/100000
            var y = coords[1]/100000;
      let xInput = document.querySelector('input[name="koor_x"]');
      let yInput = document.querySelector('input[name="koor_y"]');
      xInput.value = x;
      yInput.value = y;
      $('#kordinatmodal').modal('show');

          },
        this);
  }


var zoomIno;
map.on("moveend", function() {
    zoomInfo = map.getView().getZoom();
});